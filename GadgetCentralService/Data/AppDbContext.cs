using Microsoft.EntityFrameworkCore;
using GadgetCentralService.Models;

namespace GadgetCentralService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}