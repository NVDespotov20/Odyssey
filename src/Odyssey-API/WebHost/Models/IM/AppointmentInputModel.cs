using System.ComponentModel.DataAnnotations;
using WebHost.Entities;

namespace WebHost.Models;

public class AppointmentInputModel
{
    public string InstructorId { get; set; } = Guid.NewGuid().ToString();
    public User? Instructor { get; set; }
    
    public string StudentId { get; set; } = Guid.NewGuid().ToString();
    public User? Student { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public string Status { get; set; } = "Pending"; 
}