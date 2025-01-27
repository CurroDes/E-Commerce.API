using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class CryptoService
    {
        private readonly IConfiguration _configuration;

        public CryptoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DecryptPassword(string encryptedPassword, string key)
        {
            UtilService util = new UtilService(Encoding.ASCII.GetBytes(key));
            return util.DesCifrar(encryptedPassword, _configuration["ClaveCifrado"]);
        }
    }
}
