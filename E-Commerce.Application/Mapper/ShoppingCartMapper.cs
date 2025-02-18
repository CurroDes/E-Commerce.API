using Castle.Core.Logging;
using E_Commerce.Application.DTOs;
using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper;

public class ShoppingCartMapper
{
    private readonly IProductRepository<Product> _productRepository;
    private readonly ECommerceContext _context;
    private readonly ILogger<ShoppingCartMapper> _logger;
    public ShoppingCartMapper(IProductRepository<Product> productRepository, ECommerceContext context)
    {
        _productRepository = productRepository;
        _context = context;
    }

    public ShoppingCart MapToCart(ShoppingCartDTO sc)
    {
        return new ShoppingCart
        {
            IdProduct = sc.IdProduct,
            CreatedDate = DateTime.Now,
            Quantity = sc.Quantity
        };
    }

    public IEnumerable<ShoppingCart> CreateShoppingCart(IEnumerable<ShoppingCartDTO> detalles, Order o)
    {
        return detalles.Select(d => new ShoppingCart
        {
            Quantity = d.Quantity,
            UnitAmount = d.UnitAmount,
            IdProduct = d.IdProduct,
            IdOrder = o.Id,
            CreatedDate = DateTime.Now

        });
    }

    public async Task SubtractStockFromCart(IEnumerable<ShoppingCartDTO> cartItems)
    {
        foreach (var item in cartItems)
        {
            //obtenemos el producto de nuestra bbdd:
            var product = await _productRepository.GetProductByIdAsync(item.IdProduct);

            if (product != null)
            {
                product.Quantity -= item.Quantity;

                if (product.Quantity == 0)
                {
                    product.Stock = 0;
                }

                if (product.Quantity < 0)
                {
                    _logger.LogError($"No hay suficiente stock para el producto {product.NameProduct}. Stock actual: {product.Stock}, cantidad solicitada: {item.Quantity}");
                    throw new InvalidOperationException($"No hay suficiente stock para el producto {product.NameProduct}. Stock actual: {product.Stock}, cantidad solicitada: {item.Quantity}");
                }

            }

            await _productRepository.ModifyProductNormalAsync(product);
        }

    }
}
