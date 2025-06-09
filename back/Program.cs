using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;
using System.Data;

// Import all module interfaces
using EitechPfe.Modules.Product.Interfaces;
using EitechPfe.Modules.User.Interfaces;
using EitechPfe.Modules.Product;
using EitechPfe.Modules.User;

// Import email service
using EitechPfe.Services;
using EitechPfe.Interfaces;

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
        case "send-email":
            if (args.Length < 2)
            {
                Console.WriteLine("Please provide an email address. Usage: send-email <email>");
                return;
            }
            var emailService = new EmailService(builder.Configuration);
            emailService.SendSubscriptionReminderEmail(args[1]).Wait();
            Console.WriteLine($"Email sent to {args[1]}.");
            return;
        default:
            Console.WriteLine("Invalid command! Use 'init', 'seed', 'clean', or 'send-email'.");
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

// Register IDbConnection for DI
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

// Register Product module
builder.Services.AddScoped<EitechPfe.Modules.Product.Interfaces.IProductRepository, EitechPfe.Modules.Product.ProductRepository>();
builder.Services.AddScoped<EitechPfe.Modules.Product.Interfaces.IProductService, EitechPfe.Modules.Product.ProductService>();

// Register User module
builder.Services.AddScoped<EitechPfe.Modules.User.Interfaces.IUserRepository, EitechPfe.Modules.User.UserRepository>();
builder.Services.AddScoped<EitechPfe.Modules.User.Interfaces.IUserService, EitechPfe.Modules.User.UserService>();

// Register EmailService
builder.Services.AddScoped<IEmailService, EmailService>();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
