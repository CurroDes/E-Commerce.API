using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
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
    }
}
