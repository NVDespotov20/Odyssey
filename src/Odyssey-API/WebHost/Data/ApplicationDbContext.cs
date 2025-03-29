using Microsoft.EntityFrameworkCore;
using WebHost.Entities;

namespace WebHost.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Academy> Academies { get; set; }
    public DbSet<Image> Images { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes().ToList().ForEach(e => {
            e.SetTableName(ConvertToSnakeCase(e.Name, true));
            e.SetPrimaryKey(e.GetProperty("Id"));
            e.GetProperty("Id");
            e.GetProperties().ToList().ForEach(p => p.SetColumnName(ConvertToSnakeCase(p.Name)));
        });
        
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Academy)
                .WithMany(a => a.UserRoles)
                .HasForeignKey(ur => ur.AcademyId)
                .IsRequired(false);
            
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = Guid.NewGuid().ToString(), Name = "user" },
                    new Role { Id = Guid.NewGuid().ToString(), Name = "instructor" }
                );
    }
    private string ConvertToSnakeCase(string input, bool plural = false)
    {
        if (plural)
        {
            var newString = string.Concat(input.Split('.')[^1].Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + char.ToLowerInvariant(x) : char.ToLowerInvariant(x).ToString()));
            if (newString[^1] == 's')
                return newString + "es";
            return newString + 's';
        }
        return string.Concat(input.Split('.')[^1].Select((x, i) =>
            i > 0 && char.IsUpper(x) ? "_" + char.ToLowerInvariant(x) : char.ToLowerInvariant(x).ToString()));

    }
}