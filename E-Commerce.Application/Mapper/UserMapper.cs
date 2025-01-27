using E_Commerce.Application.DTOs;
using E_Commerce.Application.Service;
using E_Commerce.Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class UserMapper
    {
        private readonly IConfiguration _configuration;
        public UserMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User MapToUser(UserDTO u)
        {
            var claveCifrado = _configuration["ClaveCifrado"];
            byte[] keyByte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            UtilService util = new UtilService(keyByte);

            return new User
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PasswordHash = Encoding.ASCII.GetBytes(util.Cifrar(u.Password, claveCifrado)),
                RegistrationDate = DateTime.Now
            };
        }
    }
}
