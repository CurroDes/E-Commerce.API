﻿using System;
using System.Collections.Generic;

namespace E_Commerce.Data.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? DateDrop { get; set; }

    public byte[]? PasswordHash { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
