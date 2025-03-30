using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebHost.Entities;

namespace WebHost.Data;

public class OdysseyDbContext : IdentityDbContext<User>
{
    public DbSet<Academy> Academies { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    
    public OdysseyDbContext(DbContextOptions<OdysseyDbContext> options)
        : base(options)
    {
    }
}