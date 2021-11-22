
using System.Threading.Tasks;

namespace Services.ImageService
{
    public interface IImageService
    {
        Task encode(byte[] source);
    }
}
