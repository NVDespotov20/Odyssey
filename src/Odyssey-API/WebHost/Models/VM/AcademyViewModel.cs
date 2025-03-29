namespace WebHost.Models;

public class AcademyViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    
    public string? PhotoUrl { get; set; }
    
    public IEnumerable<UserViewModel> Instructors { get; set; }
}