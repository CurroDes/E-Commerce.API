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

        public async Task<T> GetProductByIdAsync(int productId)
        {
            return await _context.Set<T>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task ModifyProductNormalAsync(Product product)
        {
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
