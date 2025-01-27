using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Mapper;
using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service;

public class OrderService : IOrderService
{
    private readonly ShoppingCartMapper _shoppingCartMapper;
    private readonly IUserRepository<User> _userRepository;
    private readonly ILogger<OrderService> _logger;
    private readonly OrderMapper _orderMapper;
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
    private readonly ECommerceContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository<Product> _productRepository;
    public OrderService(ShoppingCartMapper shoppingCartMapper, IUserRepository<User> userRepository, ILogger<OrderService> logger, OrderMapper orderMapper,
        IOrderRepository<Order> orderRepository, IShoppingCartRepository<ShoppingCart> shoppingCartRepository, ECommerceContext context, IUnitOfWork unitOfWork,
        IProductRepository<Product> productRepository)
    {
        _shoppingCartMapper = shoppingCartMapper;
        _userRepository = userRepository;
        _logger = logger;
        _orderMapper = orderMapper;
        _orderRepository = orderRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _context = context;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> AddCartUser(int id, OrderDTO o)
    {

        Result result = new Result();
        result.Success = true;
        try
        {
            var userApp = await _userRepository.GetIdUserAsync(id);

            if (userApp.Id != id)
            {
                result.Success = false;
                result.Error = "El usuario no cohincide con él id de navegación";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            //Mapper la orden recibida con los productos del usuario:
            var order = _orderMapper.MapToOrder(userApp, o);

            //Después de acabar la orden, montaremos el carrito para proceder a la compra.
            var shoppingCart = _shoppingCartMapper.CreateShoppingCart(o.DetallesPedidos, o.IdUser);


            if (shoppingCart == null)
            {
                result.Success = false;
                result.Error = "Error al intentar añadir los productos en el carrito";
                _logger.LogError(result.Error.ToString());

                return result;
            }


            await _shoppingCartMapper.SubtractStockFromCart(o.DetallesPedidos);

            await _orderRepository.AddUserAsync(order);
            await _context.ShoppingCarts.AddRangeAsync(shoppingCart);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            result.Text = "Se ha añadido correctamente la orden y el carrito --- mensaje de prueba";
            _logger.LogInformation(result.Text.ToString());
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = "Error al intentar añadir los productos en el carrito";
            _logger.LogError(result.Error.ToString());
            await _unitOfWork.RollbackAsync();

            return result;
        }

        return result;
    }
}
