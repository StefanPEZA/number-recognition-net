using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.ImageService;
using System.Collections.Generic;
using NumberRecognitionAPI.Utils;

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
        public async Task<IActionResult> PredictImage(IFormFile image)
        {
            object response;
            bool valid;
            (valid, response) = Shared.CheckIfIsValidImage(image);
            if (!valid)
            {
                return BadRequest(response);
            }

            byte[] image_bytes = await Shared.IFormFileToByteArray(image);

            response = new
            {
                status = "OK",
                image_length = image.Length,
                file_name = image.FileName,
                file_type = image.ContentType,
                predicted_label = "0",
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("resize")]
        public async Task<IActionResult> ResizeImage(IFormFile image, int width, int height)
        {
            object response;
            bool valid;
            (valid, response) = Shared.CheckIfIsValidImage(image);
            if (!valid)
            {
                return BadRequest(response);
            }

            byte[] image_bytes = await Shared.IFormFileToByteArray(image);
            byte[] processed_image = await _imageService.Resize(image_bytes,width,height);

            response = new
            {
                status = "OK",
                processed_image = processed_image,
                original_image = image_bytes,
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("crop")]
        public async Task<IActionResult> CropImage(IFormFile image)
        {
            object response;
            bool valid;
            (valid, response) = Shared.CheckIfIsValidImage(image);
            if (!valid)
            {
                return BadRequest(response);
            }

            byte[] image_bytes = await Shared.IFormFileToByteArray(image);
            byte[] processed_image = await _imageService.Crop(image_bytes);

            response = new
            {
                status = "OK",
                processed_image = processed_image,
                original_image = image_bytes,
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("split")]
        public async Task<IActionResult> SplitImage(IFormFile image)
        {
            object response;
            bool valid;
            (valid, response) = Shared.CheckIfIsValidImage(image);
            if (!valid)
            {
                return BadRequest(response);
            }

            byte[] image_bytes = await Shared.IFormFileToByteArray(image);
            List<byte[]> processed_image = await _imageService.Split(image_bytes);

            response = new
            {
                status = "OK",
                processed_image = processed_image.ToArray(),
                original_image = image_bytes,
            };
            return Ok(response);
        }

    }
}
