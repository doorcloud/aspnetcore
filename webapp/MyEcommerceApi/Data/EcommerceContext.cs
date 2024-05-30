using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyEcommerceApi;
using MyEcommerceApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MyEcommerceApi.Data;

public partial class EcommerceContext : DbContext
{

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=mypassword;database=ecommerce;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<User>().HasMany(m => m.Orders);
      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
