using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using neighbor_chef.Data;
using neighbor_chef.Data.Seeds;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.TokenService;
using neighbor_chef.UnitOfWork;
using AutoMapper;
using Duende.IdentityServer.EntityFramework.Services;
using Duende.IdentityServer.Services;
using neighbor_chef.Filters;
using neighbor_chef.Models.MappingProfile;
using neighbor_chef.Services;
using neighbor_chef.Services.Cors;
using neighbor_chef.Services.Orders;
using neighbor_chef.Services.People.Chefs;
using neighbor_chef.Services.Reviews;
using ITokenService = neighbor_chef.TokenService.ITokenService;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("MyCorsPolicy",
//         builder =>
//         {
//             builder.WithOrigins("https://localhost:44475", "https://localhost:7013")
//                 .AllowAnyMethod()
//                 .AllowAnyHeader();
//         });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddSingleton<ICorsPolicyService, MyCorsPolicyService>();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
        };
    });


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(context =>
    new UnitOfWork(context.GetRequiredService<ApplicationDbContext>()));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IChefService, ChefService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IReviewsService, ReviewsService>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddScoped<CustomerAuthorizeAttribute>();
builder.Services.AddScoped<ChefAuthorizeAttribute>();


var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

logger.LogInformation("Hello, world!");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
    context.Request.Body.Position = 0;
    
    logger.LogInformation("Request: {method} {url} {body}", context.Request.Method, context.Request.Path, body);

    await next.Invoke();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.DbInitialize(context, roleManager, userManager);
}

app.Run();