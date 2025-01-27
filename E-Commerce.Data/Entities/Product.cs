using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? NameProduct { get; set; }

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
