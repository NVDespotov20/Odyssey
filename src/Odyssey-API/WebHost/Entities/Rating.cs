using System.ComponentModel.DataAnnotations;

namespace WebHost.Entities;

public class Rating
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public int Score { get; set; }
    
    public string UserId { get; set; }
    public User? User { get; set; }
}