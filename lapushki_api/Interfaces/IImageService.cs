using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Interfaces
{
    public interface IImageService
    {
        Task<IActionResult> UploadImage(IFormFile file);
    }
}
