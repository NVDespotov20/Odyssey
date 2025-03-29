using Microsoft.EntityFrameworkCore;
using WebHost.Entities;

namespace WebHost.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Academy> Academies { get; set; }
    public DbSet<Image> Images { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}