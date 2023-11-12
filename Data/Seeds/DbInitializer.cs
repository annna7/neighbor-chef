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
        // var 
        //     var chefEmails = new List<string>
        //     {
        //         "a@gmail.com",
        //         "b@gmail.com",
        //         "c@gmail.com"
        //     };
        //     
        //     foreach (var email in chefEmails)
        //     {
        //         var user = new ApplicationUser
        //         {
        //             UserName = email,
        //             Email = email,
        //             EmailConfirmed = true
        //         };
        //         var result = await userManager.CreateAsync(user, "Password123!");
        //         if (result.Succeeded)
        //         {
        //             await userManager.AddToRoleAsync(user, "Chef");
        //         }
        //         
        //         var chef = new Person
        //         {
        //             FirstName = email.Split("@")[0].Humanize(LetterCasing.Title),
        //             LastName = email.Split("@")[1].Humanize(LetterCasing.Title),
        //             ApplicationUserId = user.Id
        //         };
        //         
        //         Console.Error.WriteLine(chef);
        //         
        //         await context.People.AddAsync(chef);
        //         await context.SaveChangesAsync();
        //     }
        //     
        //     var customerEmails = new List<string>
        //     {
        //         "d@gmail.com",
        //         "e@gmail.com",
        //         "f@gmail.com"
        //     };
        //     
        //     foreach (var email in customerEmails)
        //     {
        //         var user = new ApplicationUser
        //         {
        //             UserName = email,
        //             Email = email,
        //             EmailConfirmed = true
        //         };
        //         var result = await userManager.CreateAsync(user, "Password123!");
        //         if (result.Succeeded)
        //         {
        //             await userManager.AddToRoleAsync(user, "Chef");
        //         }
        //         
        //         var customer = new Person
        //         {
        //             FirstName = email.Split("@")[0].Humanize(LetterCasing.Title),
        //             LastName = email.Split("@")[1].Humanize(LetterCasing.Title),
        //             ApplicationUserId = user.Id
        //         };
        //
        //         Console.Error.WriteLine(customer);
        //         await context.People.AddAsync(customer);
        //         await context.SaveChangesAsync();

        public static async Task DbInitialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await context.Database.EnsureCreatedAsync();
        await SeedUsers(context, userManager, roleManager);
        await SeedRoles(context, roleManager);
    }
}