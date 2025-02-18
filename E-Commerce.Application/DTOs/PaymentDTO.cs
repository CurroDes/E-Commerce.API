using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs
{
    public class PaymentDTO
    {
        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreditCart { get; set; }
        public string Cash { get; set; }
        public string Bizum { get; set; }

    }
}
