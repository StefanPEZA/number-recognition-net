
using System.Threading.Tasks;

namespace Services.ImageService
{
    public interface IImageService
    {
        Task<int[,]> EncodeAsync(byte[] source);
    }
}
