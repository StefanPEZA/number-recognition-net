﻿using Microsoft.AspNetCore.Http;
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

            await _imageService.encode(image_bytes);
  

            response = new
            {
                status = "OK",
                image_length = image.Length,
                file_name = image.FileName,
                file_type = image.ContentType,
                predicted_label = "not yet implemented"
            };
            return Ok(response);
        }

    }
}
