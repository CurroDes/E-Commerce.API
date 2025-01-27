using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Mapper;
using E_Commerce.Application.Service;
using E_Commerce.Application.UnitOfWork;
using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog(); // Usa Serilog para el registro de logs

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

// Register services
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<TokenMapper>();
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<ShoppingCartMapper>();
builder.Services.AddScoped<OrderMapper>();

builder.Services.AddScoped<GenerateTokenService>();
builder.Services.AddScoped<CryptoService>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository<User>, UserRepository<User>>();
builder.Services.AddScoped<IShoppingCartRepository<ShoppingCart>, ShoppingCartRepository<ShoppingCart>>();
builder.Services.AddScoped<IProductRepository<Product>, ProductRepository<Product>>();
builder.Services.AddScoped<IOrderRepository<Order>, OrderRepository<Order>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Agrega autenticación
app.UseAuthorization();

app.MapControllers();

app.Run();
