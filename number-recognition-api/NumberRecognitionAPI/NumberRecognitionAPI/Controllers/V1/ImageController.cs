using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.ImageService;
using System;

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


        /*[HttpPost]
        [Route("predict")]
        public async Task<IActionResult> PredictImage(IFormFile image)
        {
            object response;
            if (image == null || !image.ContentType.Contains("image"))
            {
                response = new
                {
                    status = "ERROR",
                    message = "You didn't sent an image!"
                };
                return BadRequest(response);
            }
            if (image.Length > 512_000)
            {
                response = new
                {
                    status = "ERROR",
                    message = "Image too large, you can upload files up to 500 KB!"
                };
                return BadRequest(response);
            }

            byte[] image_bytes = new byte[image.Length];
            await image.OpenReadStream().ReadAsync(image_bytes, 0, (int)image.Length);

            byte[,] processed_image = await _imageService.EncodeAsync(image_bytes);

            response = new
            {
                status = "OK",
                image_length = image.Length,
                file_name = image.FileName,
                file_type = image.ContentType,
                predicted_label = "not yet implemented",
                processed_image = _imageService.GetMatrixString()
            };
            return Ok(response);
        }*/

        [HttpPost]
        [Route("resize")]
        public async Task<IActionResult> ResizeImage(IFormFile image,int width,int height)
        {
            object response;
            if (image == null || !image.ContentType.Contains("image"))
            {
                response = new
                {
                    status = "ERROR",
                    message = "You didn't sent an image!"
                };
                return BadRequest(response);
            }
            if (image.Length > 512_000)
            {
                response = new
                {
                    status = "ERROR",
                    message = "Image too large, you can upload files up to 500 KB!"
                };
                return BadRequest(response);
            }

            byte[] image_bytes = new byte[image.Length];
            await image.OpenReadStream().ReadAsync(image_bytes, 0, (int)image.Length);
            byte[] processed_image = await _imageService.Resize(image_bytes,width,height);

            response = new
            {
                status = "OK",
                processed_image = processed_image
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("crop")]
        public async Task<IActionResult> CenterImage(IFormFile image)
        {
            object response;
            if (image == null || !image.ContentType.Contains("image"))
            {
                response = new
                {
                    status = "ERROR",
                    message = "You didn't sent an image!"
                };
                return BadRequest(response);
            }
            if (image.Length > 512_000)
            {
                response = new
                {
                    status = "ERROR",
                    message = "Image too large, you can upload files up to 500 KB!"
                };
                return BadRequest(response);
            }

            byte[] image_bytes = new byte[image.Length];
            await image.OpenReadStream().ReadAsync(image_bytes, 0, (int)image.Length);
            byte[] processed_image = await _imageService.Crop(image_bytes);

            response = new
            {
                status = "OK",
                processed_image = processed_image
            };
            return Ok(response);
        }

    }
}
