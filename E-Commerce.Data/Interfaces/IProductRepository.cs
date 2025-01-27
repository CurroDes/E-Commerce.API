using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Interfaces
{
    public interface IProductRepository<T> where T : Product
    {
        Task AddProductAsync(T product);
        Task<T> GetProductByIdAsync(int productId);

        Task ModifyProductNormalAsync(Product product);
    }
}
