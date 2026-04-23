using lapushki_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Controllers
{
    public class ImageController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        [Route("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _imageService.UploadImage(file);
        }
    }
}
