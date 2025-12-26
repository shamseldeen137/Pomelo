using Microsoft.EntityFrameworkCore;
using Pomelo.Models;

public class ApplicationDbContext :DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Optional: Add any custom model configurations here
    }
}