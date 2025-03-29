using Microsoft.EntityFrameworkCore;
using WebHost.Data;
using WebHost.Services.Contracts;
using WebHost.Entities;

namespace WebHost.Services.Implementations;

public class ImageService : IImageService
{
    private readonly IFileService fileService;
    private readonly ApplicationDbContext context;
    public ImageService(IFileService fileService, ApplicationDbContext context)
    {
        this.fileService = fileService;
        this.context = context;
    }
    
    public async Task<Image> CreateImageAsync(IFormFile file, byte[] key) 
    {
        Image image = new Image();
        string newFileName = await this.fileService.StoreFileAsync(file, key);
        image.FileName = file.FileName;
        image.Path = Path.Combine("Uploads", newFileName);  

        await this.context.Images.AddAsync(image);
        await this.context.SaveChangesAsync();
        
        return image;
    }

    public async Task DeleteImageAsync(string id)
    {
        var image = await this.context.Images.FirstOrDefaultAsync(x => x.Id == id);
        
        if (image == null)
        {
            throw new Exception("Image not found");
        }

        await this.fileService.DeleteFileAsync(image.Path);
        
        this.context.Images.Remove(image);
        await this.context.SaveChangesAsync();
    }

    public async Task<Image> GetImageAsync(string id)
    {
        var image = await this.context.Images.FirstOrDefaultAsync(x => x.Id == id);
        
        if (image == null)
        {
            throw new Exception("Image not found");
        }

        return image;
    }

    public async Task<IEnumerable<Image>> GetImagesAsync()
    {
        var images = await this.context.Images.ToListAsync();

        return images;
    }
}