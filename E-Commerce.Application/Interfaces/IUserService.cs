using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result> AddUserService(UserDTO u);
        Task<Result> LoginUser(int id, AuthApiViewModelDTO u);
    }
}
