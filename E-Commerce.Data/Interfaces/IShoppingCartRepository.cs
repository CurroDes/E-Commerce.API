using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Interfaces
{
    public interface IShoppingCartRepository<T> where T : ShoppingCart
    {
        Task AddShoppingCartAsync(T cart);
        Task<T> IsOrdenCart(int cart);
        Task ModifyCart(T shoppingCart);
        Task<IEnumerable<T>> GetCartByOrderIdAsync(int orderId);
        Task<IEnumerable<T>> UnPaidCartAsync(int idOrden);
    }
}
