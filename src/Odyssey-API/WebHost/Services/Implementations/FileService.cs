using WebHost.Helpers;
using WebHost.Services.Contracts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace WebHost.Services.Implementations;

public class FileService : IFileService
{
    private readonly List<string> allowedExtensions = new List<string>() { ".jpg", ".jpeg", ".png" };
    private readonly IEncryptionService encryptionService;
    
    public FileService(IEncryptionService encryptionService)
    {
        this.encryptionService = encryptionService;
    }
    
    public async Task<string> StoreFileAsync(IFormFile file, byte[] key)
    {
        string extension = Path.GetExtension(file.FileName);
        
        if (!this.allowedExtensions.Contains(extension))
        {
            throw new Exception($"Invalid file extension \'{extension}\'.");
        }

        long size = file.Length;

        if (size > Constants.MaxFileSize)
        {
            throw new Exception($"File size exceeds maximum allowed size of {Constants.MaxFileSize}.");
        }

        string newFileName = Guid.NewGuid().ToString() + extension;
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using var imageStream = file.OpenReadStream();
        using var image = await Image.LoadAsync(imageStream);
        
        using var ms = new MemoryStream();
        
        await image.SaveAsync(ms, new JpegEncoder() {Quality=67});
        
        var encryptedFile = this.encryptionService.Encrypt(ms.ToArray(), key);
        
        await using FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create);
        await stream.WriteAsync(encryptedFile, 0, encryptedFile.Length);

        return newFileName;
    }

    public async Task<byte[]> GetFileAsync(string path, byte[] key)
    {
        byte[] file = await System.IO.File.ReadAllBytesAsync(path);
        byte[] decryptedFile = this.encryptionService.Decrypt(file, key);
        return decryptedFile;
    }
    
    public async Task DeleteFileAsync(string path)
    {
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}