using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NumberRecognitionAPI.Utils
{
    public static class Shared
    {
        public static async Task<byte[]> IFormFileToByteArray(IFormFile image)
        {
            Memory<byte> image_bytes = new Memory<byte>(new byte[image.Length]);
            await image.OpenReadStream().ReadAsync(image_bytes);
            return image_bytes.ToArray();
        }

        public static (bool,object) CheckIfIsValidImage(IFormFile image)
        {
            object response;
            if (image == null || !image.ContentType.Contains("image"))
            {
                response = new
                {
                    status = "ERROR",
                    message = "You didn't sent an image!"
                };
                return (false, response);
            }
            if (image.Length > 512_000)
            {
                response = new
                {
                    status = "ERROR",
                    message = "Image too large, you can upload files up to 500 KB!"
                };
                return (false, response);
            }
            return (true, null);
        }
    }
}
