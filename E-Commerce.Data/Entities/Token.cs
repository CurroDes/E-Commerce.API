using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Entities
{
    public class Token
    {
        public string Token1 { get; set; }
        public DateTime CreatedAtToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
