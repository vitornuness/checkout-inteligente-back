namespace Infra.Repositories.Database;

using Core.Models;
using Microsoft.EntityFrameworkCore;

public partial class CheckoutDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Campaign> Campaigns { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Report> Reports { get; set; }

    public CheckoutDbContext()
    {
    }

    public CheckoutDbContext(DbContextOptions<CheckoutDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        this.OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
