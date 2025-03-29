using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHost.Entities;

public class Academy
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public decimal Price { get; set; }
}