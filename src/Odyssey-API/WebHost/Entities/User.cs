using System.ComponentModel.DataAnnotations.Schema;

namespace WebHost.Entities;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    [MaxLength(255)] 
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? AboutMe { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public byte[] Password { get; set; } = [];

    [Required]
    public string Salt { get; set; } = string.Empty;

    public bool? Deleted { get; set; }
}
