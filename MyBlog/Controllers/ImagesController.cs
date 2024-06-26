using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Repositories;

namespace MyBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        
        //only for file without [FromBody]
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL= await _imageRepository.UploadAsync(file);
            if (imageURL == null) {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
