using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHost.Entities;

public class Role
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; } // Roles: "user", "admin", "instructor", "academy"

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}