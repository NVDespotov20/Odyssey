using System.ComponentModel.DataAnnotations;

namespace WebHost.Models;

public class UserInputModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MaxLength(200)]
    public string? AboutMe { get; set; } 
    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}