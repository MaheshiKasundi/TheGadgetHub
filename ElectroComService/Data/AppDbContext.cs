using ElectroComService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ElectroComService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}