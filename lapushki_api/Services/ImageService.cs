using lapushki_api.Data;
using lapushki_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Файл не выбран"
                });
            }

            var fileName = file.FileName;

            var imagesFolder = Path.Combine(_environment.WebRootPath, "images");

            var filePath = Path.Combine(imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new OkObjectResult(new
            {
                status = true,
                imageUrl = $"/images/{fileName}"
            });
        }
    }
}
