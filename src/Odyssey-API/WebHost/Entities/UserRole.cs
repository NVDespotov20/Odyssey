using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebHost.Entities;
[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	
	[Required]
	public string UserId { get; set; } = Guid.NewGuid().ToString();

	[ForeignKey("UserId")]
	public User? User { get; set; }

	[Required]
	public string RoleId { get; set; } = Guid.NewGuid().ToString();

	[ForeignKey("RoleId")]
	public Role? Role { get; set; }

	public string? AcademyId { get; set; } // Nullable if not an instructor

	[ForeignKey("AcademyId")]
	public Academy? Academy { get; set; } // Nullable, linked to Academy
}
