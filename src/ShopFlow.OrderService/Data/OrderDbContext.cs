using Microsoft.EntityFrameworkCore;
using ShopFlow.OrderService.Models;

namespace ShopFlow.OrderService.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLineItem> OrderLineItems => Set<OrderLineItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(256);
            entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
            entity.Property(o => o.Status).HasConversion<string>();
            entity.HasMany(o => o.LineItems)
                  .WithOne(li => li.Order)
                  .HasForeignKey(li => li.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderLineItem>(entity =>
        {
            entity.HasKey(li => li.Id);
            entity.Property(li => li.ProductName).IsRequired().HasMaxLength(256);
            entity.Property(li => li.UnitPrice).HasPrecision(18, 2);
        });
    }
}