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
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");
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
            entity.Property(e => e.UnitAmount).HasColumnType("decimal(18, 0)");

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
