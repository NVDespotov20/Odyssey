using System.ComponentModel.DataAnnotations;

namespace WebHost.Entities;

public class Image
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FileName { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;
}