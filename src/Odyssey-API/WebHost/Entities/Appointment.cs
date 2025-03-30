namespace WebHost.Entities;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string InstructorId { get; set; } = Guid.NewGuid().ToString();
    public User? Instructor { get; set; }

    public string StudentId { get; set; } = Guid.NewGuid().ToString();
    public User? Student { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Status { get; set; } = string.Empty;
}