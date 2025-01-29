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

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(); // Usa Serilog para el registro de logs
builder.Host.UseSerilog(); // Usa Serilog para el registro de logs

// Resto de la configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

// Registro de servicios y repositorios
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

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
