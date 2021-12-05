﻿
using System.Threading.Tasks;

namespace Services.ImageService
{
    public interface IImageService
    {
        Task<byte[]> Resize(byte[] source,int width, int height);
        Task<byte[]> Crop(byte[] source);
    }
}