
using System.Threading.Tasks;

namespace Services.ImageService
{
    public class ImageService : IImageService
    {
        public async Task<int[,]> EncodeAsync(byte[] source)
        {
            return await new ImageProcessor().Encode(source);
        }
    }
}
