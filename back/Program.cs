using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EitechPfe.Interfaces;
using EitechPfe.Services;
using EitechPfe.Repositories;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Database connection string
string connectionString = builder.Configuration.GetConnectionString("MariaDbConnection") 
    ?? throw new InvalidOperationException("Database connection string is missing.");

// Handle database operations from command-line arguments
if (args.Length > 0)
{
    switch (args[0].ToLower())
    {
        case "init":
            new DatabaseInit(connectionString).Run();
            return;
        case "seed":
            new DatabaseSeed(connectionString).Run();
            return;
        case "clean":
            new DatabaseClean(connectionString).Run();
            return;
        default:
            Console.WriteLine("Invalid command! Use 'init', 'seed', or 'clean'.");
            return;
    }
}

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Database configuration
builder.Services.AddSingleton<DatabaseConfig>();

// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubscriptionOrderRepository, SubscriptionOrderRepository>();
builder.Services.AddScoped<ISubscriptionTierRepository, SubscriptionTierRepository>();
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<ILicenseOptionRepository, LicenseOptionRepository>();
builder.Services.AddScoped<ILicenseOrderRepository, LicenseOrderRepository>();
builder.Services.AddScoped<IBlackListedRepository, BlackListedRepository>();
builder.Services.AddScoped<ILicenseActivationRepository, LicenseActivationRepository>();
builder.Services.AddScoped<ILicenseBundleRepository, LicenseBundleRepository>();

// Register services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubscriptionOrderService, SubscriptionOrderService>();
builder.Services.AddScoped<ISubscriptionTierService, SubscriptionTierService>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped<ILicenseOptionService, LicenseOptionService>();
builder.Services.AddScoped<ILicenseOrderService, LicenseOrderService>();
builder.Services.AddScoped<ILicenseBundleService, LicenseBundleService>();
builder.Services.AddScoped<ILicenseActivationService, LicenseActivationService>();
builder.Services.AddScoped<IBlackListedService, BlackListedService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "EitechPFE API",
        Version = "v1",
        Description = "API for EitechPFE"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EitechPFE API v1");
    c.RoutePrefix = "swagger";
});

app.UseRouting();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
