using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.Services;
using Newtonsoft.Json;

namespace neighbor_chef.Data.Seeds;

public class DbInitializer
{
    private static Task SeedRoles(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        if (roleManager.Roles.Any()) return Task.CompletedTask;
        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Chef", NormalizedName = "CHEF" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Customer", NormalizedName = "CUSTOMER" }
        };
        foreach (var role in roles)
        {
            roleManager.CreateAsync(role).Wait();
        }
        return Task.CompletedTask;
    }

    private static async Task SeedChefs(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (context.Chefs.Any()) return;
        var gordonApplication = new ApplicationUser
        {
            Email = "gordon@gmail.com",
            EmailConfirmed = true,
            UserName = "gordon@gmail.com",
            NormalizedUserName = "gordon@gmail.com",
            PhoneNumber = "123456789",
        };
        
        await userManager.CreateAsync(gordonApplication, "Password123!");
        await userManager.AddToRoleAsync(gordonApplication, "Chef");
        
        
        var gordonAddress = new Address
        {
            StreetNumber = "21K",
            County = "London",
            Street = "Street 1",
            City = "London",
            Country = "UK",
            ZipCode = "12345",
            Apartment = "20"
        };
        
        await context.Addresses.AddAsync(gordonAddress);
        await context.SaveChangesAsync();

        var gordon = new Chef
        {
            FirstName = "Geam",
            LastName = "Ramsay",
            Description = "I'm aggressive",
            MaxOrdersPerDay = 10,
            AdvanceNoticeDays = 3,
            AddressId = gordonAddress.Id,
            ApplicationUserId = gordonApplication.Id,
            ApplicationUser = gordonApplication,
            AvailableDatesJson = JsonConvert.SerializeObject(new List<DateTime>
            {
                new DateTime(2024, 10, 1),
                new DateTime(2024, 10, 2),
                new DateTime(2024, 10, 3)
            })
        };
        
        await context.Chefs.AddAsync(gordon);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCustomers(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (context.Customers.Any()) return;
        var johnApplication = new ApplicationUser
        {
            Email = "john@gmail.com",
            EmailConfirmed = true,
            UserName = "john@gmail.com",
            PhoneNumber = "123456789",
            NormalizedUserName = "john@gmail.com"
        };
        
        await userManager.CreateAsync(johnApplication, "Password123!");
        await userManager.AddToRoleAsync(johnApplication, "Customer");

        var johnAddress = new Address
        {
            Street = "Street 20",
            City = "London",
            County = "London",
            Country = "UK",
            ZipCode = "12345",
            StreetNumber = "30",
            Apartment = "25"
        };
        
        await context.Addresses.AddAsync(johnAddress);
        await context.SaveChangesAsync();
        
        var john = new Customer
        {
            FirstName = "Johnyn",
            LastName = "Doe",
            AddressId = johnAddress.Id,
            ApplicationUserId = johnApplication.Id,
            ApplicationUser = johnApplication,
        };
        
        await context.Customers.AddAsync(john);
        await context.SaveChangesAsync();
    }

    private static Task SeedCategories(ApplicationDbContext context)
    {
        if (context.Categories.Any()) return Task.CompletedTask;
        var categories = new List<string>
        {
            "Breakfast",
            "Vegan",
            "Meat",
            "Fish",
            "Dessert",
            "Traditional",
            "Snack",
            "Soup",
            "Salad",
            "Appetizer",
            "Side Dish",
            "Drink",
            "Pizza",
            "Pasta",
            "Burger",
        };
        foreach (var category in categories)
        {
            context.Categories.Add(new Category() { Name = category });
        }
        return context.SaveChangesAsync();
    }

    private static async Task SeedMeals(ApplicationDbContext context)
    {
        if (context.Meals.Any()) return;
        var chefId = context.Chefs.FirstOrDefault().Id;
        var meals = new List<Meal>
        {
            new Meal
            {
                Name = "Pizza",
                Description = "Pizza with cheese",
                Price = 5,
                CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Pizza")?.Id ?? Guid.NewGuid(),
                ChefId = chefId,
                IngredientsJson = "[\"cheese\", \"tomatoes\", \"flour\"]",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
            new Meal
            {
                Name = "Pasta",
                Description = "Pasta with cheese",
                Price = 7,
                CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Pasta")?.Id ?? Guid.NewGuid(),
                ChefId = chefId,
                IngredientsJson = "[\"pasta\", \"cheese\", \"tomatoes\"]",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
            new Meal
            {
                Name = "Salmon",
                Description = "Salmon with roasted vegetables and teryaki sauce",
                Price = 10,
                CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Burger")?.Id ?? Guid.NewGuid(),
                ChefId = chefId,
                IngredientsJson = "[\"salmon\", \"vegetables\", \"teriyaki sauce\"]",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
                },
            };

        foreach (var meal in meals)
        {
            context.Meals.Add(meal);
        }
        
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedOrders(ApplicationDbContext context)
    {
        if (context.Orders.Any()) return;
        var customerId = await context.Customers.Select(c => c.Id).FirstOrDefaultAsync();
        var chef = await context.Chefs.Select(c => c).FirstOrDefaultAsync(c => c.AvailableDatesJson != null);
        chef.AvailableDates = JsonConvert.DeserializeObject<List<DateTime>>(chef.AvailableDatesJson);
        // var customerId = (await customerService.GetAllCustomersAsync(true))[0].Id;
        // var chef = (await chefService.GetAllChefsAsync(true))[0];
        
        var orders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                ChefId = chef.Id,
                DeliveryDate = chef.AvailableDates[0],
                Observations = "No onions please",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                ChefId = chef.Id,
                DeliveryDate = chef.AvailableDates[1],
                Observations = "please add extra teriyaki sauce to the salmon",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
        };

        var meals = await context.Meals.Where(m => m.ChefId == chef.Id).ToListAsync();

        foreach (var order in orders)
        {
            order.OrderMeals = new List<OrderMeal>();
            foreach (var meal in meals)
            {
                order.OrderMeals.Add(new OrderMeal
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    MealId = meal.Id,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                });
            }
            context.Orders.Add(order);
        }
        
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedReviews(ApplicationDbContext context)
    {
        if (context.Reviews.Any()) return;
        var customerId = await context.Customers.Select(c => c.Id).FirstOrDefaultAsync();
        var chef = await context.Chefs.Select(c => c).FirstOrDefaultAsync();
        
        var reviews = new List<Review>
        {
            new Review
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                ChefId = chef.Id,
                Rating = 5,
                Comment = "The food was absolutely delicious!",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
            new Review
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                ChefId = chef.Id,
                Rating = 4,
                Comment = "The food was quite good!",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
        };

        foreach (var review in reviews)
        {
            context.Reviews.Add(review);
        }
        
        await context.SaveChangesAsync();
    }
    
    public static async Task DbInitialize(ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        await SeedRoles(context, roleManager);
        await SeedChefs(context, userManager);
        await SeedCategories(context);
        await SeedCustomers(context, userManager);
        await SeedMeals(context);
        await SeedOrders(context);
        await SeedReviews(context);
    }
}

  