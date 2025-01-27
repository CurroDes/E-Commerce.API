using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs
{
    public class OrderDTO
    {
        public int IdUser { get; set; }
        public Decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime DateOrder { get; set; }

        public List<ShoppingCartDTO> DetallesPedidos { get; set; }
        public OrderDTO() { this.DetallesPedidos = new List<ShoppingCartDTO>(); }

    }
}
