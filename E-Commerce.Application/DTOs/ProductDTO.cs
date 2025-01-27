using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public DateTime Registration { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
