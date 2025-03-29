using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebHost.Entities;
using WebHost.Services.Contracts;

namespace WebHost.Controllers;

/// <summary>
/// Controller for managing images
/// </summary>
[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService imageService;
    private readonly IFileService fileService;
    private readonly IConfiguration configuration;
    private readonly string AESKey;
    public ImagesController(IImageService imageService, IFileService fileService, IConfiguration configuration)
    {
        this.imageService = imageService;
        this.fileService = fileService;
        this.configuration = configuration;
        this.AESKey = this.configuration["Encryption:AESKey"]!;
    }
    
    /// <summary>
    /// Upload image
    /// </summary>
    /// <param name="file">File to upload</param>
    /// <returns>Image id</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        { 
            byte[] key = Encoding.UTF8.GetBytes(this.AESKey);
            
            Image image = await this.imageService.CreateImageAsync(file, key);
            
            return Ok(image.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Get all images
    /// </summary>
    /// <returns>All images</returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetImages()
    {
        try
        {
            IEnumerable<Image> images = await this.imageService.GetImagesAsync();
            return Ok(images);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Download image by id
    /// </summary>
    /// <param name="id">Image id</param>
    /// <streams>Image contents</streams>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetImageById(string id)
    {
        try
        {
            byte[] key = Encoding.UTF8.GetBytes(this.AESKey);
            
            Image image = await this.imageService.GetImageAsync(id);
            byte[] imageData = await this.fileService.GetFileAsync(image.Path, key);
            
            return File(imageData, "image/jpeg", image.FileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Delete image by id
    /// </summary>
    /// <param name="id">Image id</param>
    /// <returns>OK 200 if successfully deleted</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteImage(string id)
    {
        try
        {
            await this.imageService.DeleteImageAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}