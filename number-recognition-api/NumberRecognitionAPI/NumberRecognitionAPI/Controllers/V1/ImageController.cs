using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.ImageService;

namespace NumberRecognitionAPI.Controllers.V1
{            
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
                                           
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }


        [HttpPost]
        [Route("predict")]
        public async Task<IActionResult> PredictImage(IFormFile file)
        {
            object response;
            if (file == null || !file.ContentType.Contains("image"))
            {
                response = new
                {
                    status = "ERROR",
                    message = "You didn't sent an image!"
                };
                return BadRequest(response);
            }
            if (file.Length > 512_000)
            {
                response = new
                {
                    status = "ERROR",
                    message = "Image too large, you can upload files up to 500 KB!"
                };
                return BadRequest(response);
            }

            byte[] image = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(image, 0, (int)file.Length);

           await _imageService.encode(image);
  

            response = new
            {
                status = "OK",
                image_length = file.Length,
                file_name = file.FileName,
                file_type = file.ContentType,
                predicted_label = "not yet implemented"
            };
            return Ok(response);
        }

    }
}
