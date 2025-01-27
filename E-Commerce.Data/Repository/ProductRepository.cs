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
    public class ProductRepository<T> : IProductRepository<T> where T : Product
    {
        private readonly ECommerceContext _context;
        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(T product)
        {
            await _context.AddAsync(product);
        }
    }
}
