using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Repository
{
    public class OrderRepository<T> : IOrderRepository<T> where T : Order
    {
        private readonly ECommerceContext _context;
        public OrderRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(T order)
        {
            await _context.AddAsync(order);
        }

        public async Task ModifyOrderAsync(T order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task<T> IsOrderPlacedByUserAsync(int idOrder, int idUser)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(o => o.Id == idOrder && o.IdUser == idUser);
        }
    }
}
