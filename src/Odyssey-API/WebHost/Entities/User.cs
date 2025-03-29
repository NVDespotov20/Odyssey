using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebHost.Entities;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    [MaxLength(255)] 
    public string LastName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? AboutMe { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Experience { get; set; } = string.Empty;
    public bool? Deleted { get; set; }
}
