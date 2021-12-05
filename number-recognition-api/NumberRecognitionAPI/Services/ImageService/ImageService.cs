
using System.Threading.Tasks;

namespace Services.ImageService
{
    public class ImageService : IImageService
    {
        public async Task<byte[]> Resize(byte[] source,int width, int height)
        {
            return await new ImageProcessor(source).Resize(width,height);
        }

        public async Task<byte[]> Crop(byte[] source)
        {

            return await new ImageProcessor(source).Crop();
        }

    }
}
