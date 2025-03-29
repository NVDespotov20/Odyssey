using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHost.Entities;

public class RefreshToken
{
    [MaxLength(100)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string RefreshTokenValue { get; set; } = string.Empty;

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public User? User { get; set; }
}