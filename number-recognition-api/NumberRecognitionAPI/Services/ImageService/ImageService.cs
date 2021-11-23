
using System.Threading.Tasks;

namespace Services.ImageService
{
    public class ImageService : IImageService
    {
        ImageProcessor imageProcessor;
        public ImageService()
        {
            imageProcessor = new ImageProcessor();
        }

        public async Task<byte[,]> EncodeAsync(byte[] source)
        {
            return await imageProcessor.Encode(source);
        }

        public string GetMatrixString()
        {
            return imageProcessor.ToString();
        }
    }
}
