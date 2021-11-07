using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NumberRecognitionAPI.Controllers
{
    [Route("api/v1/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        [Route("predict")]
        public IActionResult PredictImage(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest(new { status = "ERROR", message = "You didn't sent an image!" });
            }
            if (file.Length > 1_000_000)
            {
                return BadRequest(new { status = "ERROR",
                    message = "Image too large, you can upload files up to 1 MB!" });
            }

            byte[] image = new byte[file.Length];
            file.OpenReadStream().Read(image, 0, (int)file.Length);

            return Ok(new { status = "OK", image_length = file.Length,
                            file_name = file.FileName,
                            predicted_label = "unspecified" });
        }

    }
}
