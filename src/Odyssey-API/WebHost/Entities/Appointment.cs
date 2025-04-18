using System.ComponentModel.DataAnnotations;

namespace WebHost.Entities;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
   
    [Required]
    public string InstructorId { get; set; } = Guid.NewGuid().ToString();
    public User? Instructor { get; set; }
    
    [Required]
    public string StudentId { get; set; } = Guid.NewGuid().ToString();
    public User? Student { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    
    [Required]
    public string Status { get; set; } = string.Empty;
}