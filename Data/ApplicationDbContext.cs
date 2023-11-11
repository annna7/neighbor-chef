using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using neighbor_chef.Models;

namespace neighbor_chef.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }
    
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    
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
    }
}