using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ImageService
{
    public interface IImageService
    {
        Task<byte[]> Resize(byte[] source,int width, int height);
        Task<byte[]> Crop(byte[] source);
        Task<List<byte[]>> Split(byte[] source);
        Task<float> Predict(byte[] source);
    }
}
