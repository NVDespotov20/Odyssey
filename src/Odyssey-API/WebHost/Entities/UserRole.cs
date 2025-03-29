using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHost.Entities;
public class UserRole
{
    [Required]
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    public string RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    public string? AcademyId { get; set; } // Nullable if not an instructor

    [ForeignKey("AcademyId")]
    public Academy? Academy { get; set; } // Nullable, linked to Academy
}