
using WebHost.Entities;

namespace WebHost.Services.Contracts;

public interface IImageService
{
    /// <summary>
    /// Create image from file and store it in the database.
    /// </summary>
    /// <param name="file">File to be stored</param>
    /// <returns>Image object</returns>
    Task<Image> CreateImageAsync(IFormFile file, byte[] key);
    
    /// <summary>
    /// Get image by id.
    /// </summary>
    /// <param name="id">Id of the image</param>
    /// <returns>Image object</returns>
    Task<Image> GetImageAsync(string id);
    
    /// <summary>
    /// Get all images from the database.
    /// </summary>
    /// <returns>Collection of Image objects</returns>
    Task<IEnumerable<Image>> GetImagesAsync();
    
    /// <summary>
    /// Delete image by id.
    /// </summary>
    /// <param name="id">Id of the image</param>
    /// <returns></returns>
    Task DeleteImageAsync(string id);
}