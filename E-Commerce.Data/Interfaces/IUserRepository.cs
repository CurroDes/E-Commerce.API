using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Interfaces
{
    public interface IUserRepository<T> where T : User
    {
        Task AddUserAsync(T userApp);
        Task<T> GetIdUserAsync(int id);
        Task<T> GetUserEmailAsync(string email);
        Task<bool> CheckEmailAsync(string email);
    }
}
