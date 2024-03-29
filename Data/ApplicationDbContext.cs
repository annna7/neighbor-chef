﻿using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Humanizer;
using neighbor_chef.Models;
using neighbor_chef.Models.Base;

namespace neighbor_chef.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Meal> Meals { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Chef> Chefs { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderMeal> OrderMeals { get; set; } = null!;
    public DbSet<FirebaseToken> FirebaseTokens { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.LogTo(Console.WriteLine);
        // optionsBuilder.EnableSensitiveDataLogging();
        // optionsBuilder.EnableDetailedErrors();
        // optionsBuilder.UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Person>(userEntity =>
        {
            userEntity.HasOne(u => u.ApplicationUser)
                .WithOne()
                .HasForeignKey<Person>(u => u.ApplicationUserId);

            userEntity.HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);
        });
        
        builder.Entity<Meal>(mealEntity =>
        {
            mealEntity.HasOne(m => m.Category)
                .WithMany(c => c.Meals)
                .HasForeignKey(m => m.CategoryId);
        });
        
        builder.Entity<Review>(reviewEntity =>
        {
            reviewEntity.HasOne(r => r.Chef)
            .WithMany(c => c.ReviewsReceived)
            .HasForeignKey(r => r.ChefId)
            .OnDelete(DeleteBehavior.NoAction);
                 
            reviewEntity.HasOne(r => r.Customer)
            .WithMany(p => p.ReviewsLeft)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
        });
        
        builder.Entity<OrderMeal>()
            .HasKey(om => new { om.OrderId, om.MealId }); 
        
        builder.Entity<OrderMeal>()
            .HasOne(om => om.Order)
            .WithMany(o => o.OrderMeals)
            .HasForeignKey(om => om.OrderId);
        
        builder.Entity<OrderMeal>()
            .HasOne(om => om.Meal)
            .WithMany(m => m.OrderMeals)
            .HasForeignKey(om => om.MealId);
        
        builder.Entity<Order>()
            .HasOne(o => o.Chef)
            .WithMany(c => c.OrdersReceived)
            .HasForeignKey(o => o.ChefId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.OrdersPlaced)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Person>()
            .HasMany(p => p.FirebaseTokens)
            .WithMany(ft => ft.People);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));
        
        foreach (var entityEntry in entries)
        {
            var baseEntity = ((BaseEntity) entityEntry.Entity);
            baseEntity.DateModified = DateTime.UtcNow;
            if (entityEntry.State == EntityState.Added || baseEntity.DateCreated == null)
            {
                baseEntity.DateCreated = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}