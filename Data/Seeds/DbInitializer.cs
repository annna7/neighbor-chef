using Humanizer;
using Microsoft.AspNetCore.Identity;
using neighbor_chef.Models;

namespace neighbor_chef.Data.Seeds;

public class DbInitializer
{
    private static Task SeedRoles(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        if (context.Roles.Any()) return Task.CompletedTask;
        var roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Chef", NormalizedName = "CHEF" },
            new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
        };
        foreach (var role in roles)
        {
            roleManager.CreateAsync(role).Wait();
        }
        return Task.CompletedTask;
    }
    
    // seed meal categories
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

    private static async Task SeedUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var chefEmails = new List<string>
        {
            "a@gmail.com",
            "b@gmail.com",
            "c@gmail.com"
        };

        foreach (var email in chefEmails)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Password123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Chef");
            }
        }

        var customerEmails = new List<string>
        {
            "d@gmail.com",
            "e#gmail.com",
            "f@gmail.com"
        };
        
        foreach (var email in customerEmails)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Password123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }
        }
    }

        public static async Task DbInitialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await context.Database.EnsureCreatedAsync();
        await SeedRoles(context, roleManager);
        await SeedCategories(context);
        // await SeedUsers(context, userManager, roleManager);
    }
}