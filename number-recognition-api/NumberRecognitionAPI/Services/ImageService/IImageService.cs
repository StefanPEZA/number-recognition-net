
using System.Threading.Tasks;

namespace Services.ImageService
{
    public interface IImageService
    {
        Task<byte[,]> EncodeAsync(byte[] source);
        string GetMatrixString();
    }
}
