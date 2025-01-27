using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? DateOrder { get; set; }
}
