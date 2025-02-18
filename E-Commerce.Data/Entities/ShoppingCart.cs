using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? IdProduct { get; set; }

    public decimal? UnitAmount { get; set; }

    public int? Quantity { get; set; }

    public int? IdOrder { get; set; }

    public string? Payment { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
