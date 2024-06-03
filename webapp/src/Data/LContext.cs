using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapp.src;
using webapp.src.Data.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace webapp.src.Data;

public partial class LContext(DbContextOptions<LContext> options, IConfiguration config) : DbContext(options)
{

    private readonly IConfiguration _config = config;

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseMySql(_config.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8_general_ci").HasCharSet("utf8");

        modelBuilder.Entity<User>().HasMany(m => m.Orders);
        modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsConcurrencyToken();
        modelBuilder.Entity<Order>().Property(p => p.RowVersion).IsConcurrencyToken();
      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
