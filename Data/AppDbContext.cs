using Microsoft.EntityFrameworkCore;
using WareHouse.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<DelayOrder> DelayOrders { get; set; }
    public DbSet<DetailRequest> DetailRequests { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TimeSheet> TimeSheets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ItemRequest> ItemRequests { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<DetailOrder> DetailOrders { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OutHistory> OutHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DetailOrder>()
            .Property(d => d.Price)
            .HasColumnType("decimal(18,2)");

        // Tránh multiple cascade paths cho DetailOrder
        modelBuilder.Entity<DetailOrder>()
            .HasOne(d => d.Products)
            .WithMany(p => p.DetailOrders)
            .OnDelete(DeleteBehavior.Restrict);

        // Tránh multiple cascade paths cho Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Suppliers)
            .WithMany(s => s.Products)
            .OnDelete(DeleteBehavior.Restrict);
    }
}