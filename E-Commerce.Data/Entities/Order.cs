using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? DateOrder { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
