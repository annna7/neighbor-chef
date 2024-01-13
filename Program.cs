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
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.OpenApi.Models;
using neighbor_chef.Filters;
using neighbor_chef.Models.MappingProfile;
using neighbor_chef.Services;
using neighbor_chef.Services.Cors;
using neighbor_chef.Services.Notifications;
using neighbor_chef.Services.Orders;
using neighbor_chef.Services.People.Chefs;
using neighbor_chef.Services.Reviews;
using ITokenService = neighbor_chef.TokenService.ITokenService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Neighbor Chef API", Version = "v1" });
});

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
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
        };
    });
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
builder.Services.AddScoped<IFirebaseNotificationService, FirebaseNotificationService>();

builder.Services.AddScoped<CustomerAuthorizeAttribute>();
builder.Services.AddScoped<ChefAuthorizeAttribute>();


var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("C:\\Users\\Acer\\Desktop\\NEIGHBOR CHEF SECRET\\neighbor-chef-93af6-firebase-adminsdk-yqe31-7092342535.json"),
        ProjectId = "neighbor-chef-93af6"
    });
}

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neighbor Chef API V1");
    c.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}

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