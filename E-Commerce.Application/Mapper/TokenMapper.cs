using E_Commerce.Application.DTOs;
using E_Commerce.Application.Service;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class TokenMapper
    {
        private readonly GenerateTokenService _generateTokenService;
        public TokenMapper(GenerateTokenService generateTokenService)
        {
            _generateTokenService = generateTokenService;
        }

        public Token MapToToken(Token t, AuthApiViewModelDTO a)
        {
            t.Token1 = _generateTokenService.GenerateToken(a);
            t.CreatedAtToken = DateTime.Now;
            t.ExpirationDate = DateTime.Now.AddHours(24);

            return t;
        }
    }
}
