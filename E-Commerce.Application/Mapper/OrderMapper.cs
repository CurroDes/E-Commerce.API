using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class OrderMapper
    {
        public OrderMapper()
        {

        }

        public Order MapToOrder(User u, OrderDTO o)
        {
            return new Order
            {
                TotalAmount = o.DetallesPedidos.Sum(d => d.Quantity * d.UnitAmount),
                Status = o.Status,
                IdUser = u.Id,
                DateOrder = DateTime.Now
            };
        }
    }
}
