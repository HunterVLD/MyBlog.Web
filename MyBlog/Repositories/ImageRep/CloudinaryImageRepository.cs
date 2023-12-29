using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Repositories;

public class CloudinaryImageRepository : IImageRepository
{
    private readonly IConfiguration _configuration;
    private readonly Account _accountCloudinary;
    
    public CloudinaryImageRepository(IConfiguration configuration)
    {
        _configuration = configuration;

        _accountCloudinary = new Account(
            _configuration.GetSection("Cloudinary")["CloudName"],
            _configuration.GetSection("Cloudinary")["ApiKey"],
            _configuration.GetSection("Cloudinary")["ApiSecret"]);
    }
    
    [HttpPost]
    public async Task<string?> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(_accountCloudinary);
        var uploadParams = new ImageUploadParams{
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            DisplayName = file.FileName
            
        };
        var uploadResult = await client.UploadAsync(uploadParams);

        if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            return uploadResult.SecureUrl.ToString();

        return null;
    }
}