using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdUser { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreditCard { get; set; }

    public string? Cash { get; set; }

    public string? Bizum { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
