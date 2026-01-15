using Microsoft.EntityFrameworkCore;
using Torico.Shared.Models;

namespace Torico.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
}
