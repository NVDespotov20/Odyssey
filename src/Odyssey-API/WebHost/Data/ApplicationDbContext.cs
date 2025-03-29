using Microsoft.EntityFrameworkCore;
using WebHost.Entities;

namespace WebHost.Data;

public class ApplicationDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    
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