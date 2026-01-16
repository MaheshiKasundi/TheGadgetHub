using Microsoft.EntityFrameworkCore;
using TechWorldService.Models;

namespace TechWorldService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; } // This creates the Products table
}