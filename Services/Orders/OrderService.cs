using Castle.Components.DictionaryAdapter.Xml;
using CorePush.Firebase;
using neighbor_chef.Exceptions.Meals;
using neighbor_chef.Exceptions.Orders;
using neighbor_chef.Exceptions.People;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Orders;
using neighbor_chef.Services.Emails;
using neighbor_chef.Services.Notifications;
using neighbor_chef.Specifications;
using neighbor_chef.Specifications.Orders;
using neighbor_chef.Specifications.People.Customers;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChefService _chefService;
    private readonly IEmailService _emailService;
    private readonly IFirebaseNotificationService _notificationService;
    
    public OrderService(IUnitOfWork unitOfWork, IChefService chefService, IFirebaseNotificationService notificationService, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _chefService = chefService;
        _emailService = emailService;
        _notificationService = notificationService;
    }

    public async Task<Order> CreateOrderAsync(Guid customerId, Guid chefId, CreateOrderDto orderDto)
    {
        var customer = await _unitOfWork.GetRepository<Customer>().
            FindFirstOrDefaultWithSpecificationPatternAsync(new FullCustomerWithIdSpecification(customerId));
        if (customer == null)
        {
            throw new CustomerNotFoundException();
        }
        
        var chef = await _unitOfWork.GetRepository<Chef>()
            .FindFirstOrDefaultWithSpecificationPatternAsync(new FullChefWithIdSpecification(chefId));
        
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        
        var orderMeals = new List<OrderMeal>();
        
        foreach (var mealWithQuantity in orderDto.MealWithQuantities)
        {
            var meal = await _unitOfWork.GetRepository<Meal>().GetByIdAsync(mealWithQuantity.MealId);
            if (meal == null)
            {
                throw new MealNotFoundException();
            }
            if (meal.ChefId != chef.Id)
            {
                throw new MealBelongsToAnotherChefException(meal.Id, meal.ChefId, chef.Id);
            }
            
            orderMeals.Add(new OrderMeal
            {
                Id = Guid.NewGuid(),
                MealId = meal.Id,
                Quantity = mealWithQuantity.Quantity,
            });
        }
       
        var deliveryDate = new DateTime(orderDto.DeliveryDate.Year, orderDto.DeliveryDate.Month, orderDto.DeliveryDate.Day,
            orderDto.DeliveryTime.Hour, orderDto.DeliveryTime.Minute, 0);
        
        if (!await _chefService.IsDateAvailable(chef, deliveryDate))
        {
            throw new InvalidOrderDateException(deliveryDate, chef.Id, customerId);
        }
        
        var order = new Order
        {
            ChefId = chefId,
            CustomerId = customerId,
            DeliveryDate = new DateTime(orderDto.DeliveryDate.Year, orderDto.DeliveryDate.Month, orderDto.DeliveryDate.Day,
                orderDto.DeliveryTime.Hour, orderDto.DeliveryTime.Minute, 0),
            Status = OrderStatus.Placed,
            Observations = orderDto.Observations,
            OrderMeals = orderMeals,
        };
        
        await _unitOfWork.GetRepository<Order>().AddAsync(order);
        await _unitOfWork.CompleteAsync();
        
        var title = "New Order";
        var body = "You have a new order from " + customer.FirstName + " " + customer.LastName + "!";
        await _notificationService.SendNotificationToPerson(chefId, title, body);
        await _emailService.SendEmailAsync(chef.ApplicationUser.Email, customer.ProfilePictureUrl, body);
        
        customer.OrdersPlaced.Add(order);
        chef.OrdersReceived.Add(order);
        await _unitOfWork.GetRepository<Customer>().UpdateAsync(customer);
        await _unitOfWork.CompleteAsync();
        await _unitOfWork.GetRepository<Chef>().UpdateAsync(chef);
        await _unitOfWork.CompleteAsync();
        
        return order;
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _unitOfWork.GetRepository<Order>().FindFirstOrDefaultWithSpecificationPatternAsync(
            new OrderWithOrderMealsSpecification(orderId));
        if (order == null)
        {
            throw new OrderNotFoundException();
        }
        return order;
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, bool isChef)
    {
        var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(orderId);
        if (order == null)
        {
            throw new OrderNotFoundException();
        }
        
        switch (isChef)
        {
            case true when !(order.Status == OrderStatus.Placed && newStatus == OrderStatus.Preparing)
                           && !(order.Status == OrderStatus.Preparing && newStatus == OrderStatus.Ready):
                throw new InvalidOrderStatusTransitionException(order.Status, newStatus);
            case false when !(order.Status == OrderStatus.Ready && newStatus == OrderStatus.Delivered):
                throw new InvalidOrderStatusTransitionException(order.Status, newStatus);
        }
        
        var chef = await _unitOfWork.GetRepository<Chef>().FindFirstOrDefaultWithSpecificationPatternAsync(
            new FullChefWithIdSpecification(order.ChefId), true);
        
        if (chef == null)
        {
            throw new ChefNotFoundException();
        }
        
        var customer = await _unitOfWork.GetRepository<Customer>().FindFirstOrDefaultWithSpecificationPatternAsync(
            new FullCustomerWithIdSpecification(order.CustomerId), true);
        
        if (customer == null)
        {
            throw new CustomerNotFoundException();
        }

        string statusText;
        
        switch (newStatus)
        {
            case OrderStatus.Preparing:
                statusText = "Chef" + " " + chef.FirstName + " " + chef.LastName + " started preparing your order!";
                await _notificationService.SendNotificationToPerson(order.CustomerId, "Status Update", statusText);
                await _emailService.SendEmailAsync(customer.ApplicationUser.Email, chef.ProfilePictureUrl, statusText);
                break;
            case OrderStatus.Ready:
                statusText = "Chef" + " " + chef.FirstName + " " + chef.LastName + " finished preparing your order! Time to pick it up!";
                await _notificationService.SendNotificationToPerson(order.CustomerId, "Status Update", statusText);
                await _emailService.SendEmailAsync(customer.ApplicationUser.Email, chef.ProfilePictureUrl, statusText);
                break;
            case OrderStatus.Delivered:
                statusText = "Chef" + " " + chef.FirstName + " " + chef.LastName + " delivered your order! Enjoy!";
                await _notificationService.SendNotificationToPerson(order.ChefId, "Status Update", statusText);
                await _emailService.SendEmailAsync(chef.ApplicationUser.Email, customer.ProfilePictureUrl, statusText);
                break;
        }
        
        order.Status = newStatus;
        await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task<Order> UpdateOrderAsync(Guid orderId, UpdateOrderDto orderDto)
    {
        var order = await _unitOfWork.GetRepository<Order>().GetByIdNoTrackingAsync(orderId);

        if (order == null)
        {
            throw new OrderNotFoundException();
        }

        if (order.Status != OrderStatus.Placed)
        {
            throw new OrderCannotBeModifiedException(order.Id, order.Status);
        }

        if (orderDto.MealIds != null)
        {
            var meals = new List<Meal>();
            foreach (var mealId in orderDto.MealIds)
            {
                var meal = await _unitOfWork.GetRepository<Meal>().GetByIdNoTrackingAsync(mealId);
                if (meal == null)
                {
                    throw new MealNotFoundException();
                }
                if (meal.ChefId != order.ChefId)
                {
                    throw new MealBelongsToAnotherChefException(meal.Id, meal.ChefId, order.ChefId);
                }
                meals.Add(meal);
            }
            
            order.OrderMeals.Clear();
            foreach (var meal in meals)
            {
                order.OrderMeals.Add(new OrderMeal
                {
                    MealId = meal.Id,
                    OrderId = order.Id,
                });
            }
        }
        if (orderDto.DeliveryDate != null || orderDto.DeliveryTime != null)
        {
            order.DeliveryDate = new DateTime(
                orderDto.DeliveryDate?.Year ?? order.DeliveryDate.Year,
                orderDto.DeliveryDate?.Month ?? order.DeliveryDate.Month,
                orderDto.DeliveryDate?.Day ?? order.DeliveryDate.Day,
                orderDto.DeliveryTime?.Hour ?? order.DeliveryDate.Hour,
                orderDto.DeliveryTime?.Minute ?? order.DeliveryDate.Minute,
                0);
        }
        
        order.Observations = orderDto.Observations ?? order.Observations;
        
        await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
        await _unitOfWork.CompleteAsync();
        
        return order;
    }

    public async Task DeleteOrderAsync(Guid customerId, Guid orderId)
    {
        var customer = await _unitOfWork.GetRepository<Customer>().GetByIdNoTrackingAsync(customerId);
        if (customer == null)
        {
            throw new CustomerNotFoundException();
        }
        
        var order = await _unitOfWork.GetRepository<Order>().GetByIdNoTrackingAsync(orderId);

        if (order == null)
        {
            throw new OrderNotFoundException();
        }
        
        if (order.CustomerId != customer.Id)
        {
            throw new OrderDoesNotBelongToCustomerException(order.Id, order.CustomerId, customer.Id);
        }

        await _unitOfWork.GetRepository<Order>().DeleteAsync(order);
        await _unitOfWork.CompleteAsync();
    }
}
