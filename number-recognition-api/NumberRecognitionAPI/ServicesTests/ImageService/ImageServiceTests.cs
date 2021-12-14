using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.ImageService;
using ServicesTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageService.Tests
{
    [TestClass()]
    public class ImageServiceTests
    {

        private readonly IImageService _imageService;

        public ImageServiceTests()
        {
             _imageService = Helper.GetRequiredService<IImageService>() ?? throw new ArgumentNullException(nameof(IImageService));
        }
        [TestMethod()]
        public async Task Resize_ShouldNotReturnNull_WhenWidthAndHeightAre28Async()
        {
            FileInfo fileInfo = new FileInfo(@".\..\..\..\resources\nine_uncentered.png");
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            Assert.IsNotNull(await _imageService.Resize(data, 28, 28));
        }

        [TestMethod()]
        public async Task Crop_ShouldNotReturnNullAsync()
        {
            FileInfo fileInfo = new FileInfo(@".\..\..\..\resources\nine_uncentered.png");
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            Assert.IsNotNull(await _imageService.Crop(data));
        }

        [TestMethod()]
        public async Task Split_ShouldNotReturnNullAsync()
        {
            FileInfo fileInfo = new FileInfo(@".\..\..\..\resources\nine_uncentered.png");
            byte[] data = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            Assert.IsNotNull(await _imageService.Split(data));
        }
    }
}