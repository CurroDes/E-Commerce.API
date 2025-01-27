using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class ShoppingCartMapper
    {
        public ShoppingCartMapper()
        {
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

        public IEnumerable<ShoppingCart> CreateShoppingCart(IEnumerable<ShoppingCartDTO> detalles, int idOrder)
        {
            return detalles.Select(d => new ShoppingCart
            {
                Quantity = d.Quantity,
                UnitAmount = d.UnitAmount,
                IdProduct = d.IdProduct,
                IdOrder = idOrder,
                CreatedDate = DateTime.Now
            });
        }
    }
}
