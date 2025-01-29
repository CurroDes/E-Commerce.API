using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class AuthenticationService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly CryptoService _cryptoService;
        public AuthenticationService(IUserRepository<User> userRepository, CryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<User> AuthenticationAsync(string email, string password)
        {
            var userApp = await _userRepository.GetUserEmailAsync(email);
            if (userApp == null || password != _cryptoService.DecryptPassword(Encoding.ASCII.GetString(userApp.PasswordHash), "clave_de_cifrado"))
            {
                throw new Exception("Credenciales incorrectas");
            }

            return userApp;
        }
    }
}
