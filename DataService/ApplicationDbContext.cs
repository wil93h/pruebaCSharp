using Microsoft.EntityFrameworkCore;
using pruebaCSharp.Entities;

namespace pruebaCSharp.DataService;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
}