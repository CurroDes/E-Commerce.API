using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int UserId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? TransactionId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ShoppingCart Cart { get; set; } = null!;
}
