
using System.Threading.Tasks;

namespace Services.ImageService
{
    public class ImageService : IImageService
    {
        public async Task encode(byte[] source)
        {
            await new ImageProcessor().encode(source);
        }
    }
}
