using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs
{
    public class ShoppingCartDTO
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdProduct { get; set; }
        public decimal UnitAmount { get; set; }
        public int Quantity { get; set; }

    }
}
