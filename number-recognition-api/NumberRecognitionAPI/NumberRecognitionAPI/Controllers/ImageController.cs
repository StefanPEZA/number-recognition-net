﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (file.Length > 100000)
            {
                return BadRequest(new { status = "ERROR",
                    message = "Image too large, you can upload files up to 100 KB!" });
            }

            byte[] image = new byte[file.Length];
            file.OpenReadStream().Read(image, 0, (int)file.Length);

            return Ok(new { status = "OK", image_length = file.Length,
                            file_name = file.FileName,
                            predicted_label = "unspecified" });
        }

    }
}
