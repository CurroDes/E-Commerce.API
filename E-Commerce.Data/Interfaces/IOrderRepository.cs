using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Interfaces
{
    public interface IOrderRepository<T> where T : Order
    {
        Task AddUserAsync(T order);
        Task<bool> IsOrderPlacedByUserAsync(int idOrder, int idUser);
        Task<bool> IsOrdenPayment(int idOrder);
    }
}
