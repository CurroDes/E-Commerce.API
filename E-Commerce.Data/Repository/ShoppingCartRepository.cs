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
    public class ShoppingCartRepository<T> : IShoppingCartRepository<T> where T : ShoppingCart
    {
        private readonly ECommerceContext _context;
        public ShoppingCartRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task AddShoppingCartAsync(T cart)
        {
            await _context.Set<T>()
                .AddRangeAsync(cart);
        }

        public async Task<T> IsOrdenCart(int cart)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(o => o.IdOrder == cart);
        }

        public async Task ModifyCart(T shoppingCart)
        {
            _context.Entry(shoppingCart).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetCartByOrderIdAsync(int orderId)
        {
            return await _context.Set<T>()
                .Where(sc => sc.IdOrder == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> UnPaidCartAsync(int idOrden)
        {
            return await _context.Set<T>()
                .Where(s => s.IdOrder == idOrden && s.Payment.ToLower() != "pagado")
                .ToListAsync();
        }
    }
}
