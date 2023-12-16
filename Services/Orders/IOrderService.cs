using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Orders;

namespace neighbor_chef.Services.Orders;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid customerId, Guid chefId, CreateOrderDto orderDto);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task<Order> UpdateOrderAsync(Guid orderId, UpdateOrderDto orderDto);
    Task DeleteOrderAsync(Guid customerId, Guid orderId);
    Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, bool isChef);
}