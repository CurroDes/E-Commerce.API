using System;
using System.Collections.Generic;
using E_Commerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Data;

public partial class ECommerceContext : DbContext
{
    public ECommerceContext()
    {
    }

    public ECommerceContext(DbContextOptions<ECommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4UIV9D8;Initial Catalog=E_COMMERCE_API;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC077A7C2D56");

            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(500);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC0766F10A99");

            entity.ToTable("Payment");

            entity.Property(e => e.Bizum).HasMaxLength(255);
            entity.Property(e => e.Cash).HasMaxLength(255);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK_Payment_Order");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Payment_User");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0700A307AE");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shopping__3214EC07B91E88CC");

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Payment).HasMaxLength(10);
            entity.Property(e => e.UnitAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK_ShoppingCart_Orders");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("ShoppingCart_IdProduct_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07938893D8");

            entity.Property(e => e.DateDrop).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FirstName).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(500);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
