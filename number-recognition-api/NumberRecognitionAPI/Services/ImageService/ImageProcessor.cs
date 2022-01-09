using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
namespace Services.ImageService
{
    class ImageProcessor
    {
        private readonly Image<Rgba32> image;
        private readonly int[,] _imageMatrix;

        public ImageProcessor(byte[] source)
        {
            image = Image.Load<Rgba32>(source);
            _imageMatrix = GetPixelMatrixFromImage();
        }

        private int[,] GetPixelMatrixFromImage()
        {
            int[,] result = new int[image.Height, image.Width];
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    
                    Rgba32 pixel = image[j, i];
                    if (pixel.R <= 45 || pixel.G <= 45 || pixel.B <= 45)
                    {
                        result[i, j] = pixel.A;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
            }
            return result;
        }

        private Image<Rgba32> FillToAspectRatio(int width, int height)
        {
            double ratioH = Math.Max((double)width / image.Width, (double)image.Width / width);
            double ratioW = Math.Max((double)height / image.Height, (double)image.Height / height);
            Size newSize = new Size((int)(width * ratioW) + (image.Width / 2), (int)(height * ratioH) + (image.Height / 2));
            Image<Rgba32> newImage = new Image<Rgba32>(newSize.Width, newSize.Height);
            
            newImage.Mutate(ic =>
            {
                ic.Fill(Color.White, new RectangleF(0, 0, newSize.Width, newSize.Height));
                ic.DrawImage(image, new Point((newSize.Width / 2) - (image.Width / 2), (newSize.Height / 2) - (image.Height / 2)), 1);
            });

            return newImage;
        }

        public async Task<byte[]> Resize(int width, int height)
        {
            Image<Rgba32> newImage = await Task.Run(() => FillToAspectRatio(width, height));
            newImage.Mutate(ic =>
            {
                ic.Resize(width, height);
            });
            
            var stream = new MemoryStream();
            newImage.SaveAsPng(stream);

            newImage.Dispose();
            
            return stream.ToArray();
        }


        public Task<(int, int, int, int)> ComputeBounderies()
        {
            int minW = image.Width;
            int maxW = 0;

            int minH = image.Height;
            int maxH = 0;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (_imageMatrix[i, j] == 255)
                    {
                        maxW = ComputeMaxW(i, j, maxW);
                        minW = ComputeMinW(i, j, minW);
                        maxH = ComputeMaxH(i, j, maxH);
                        minH = ComputeMinH(i, j, minH);
                    }
                }
            }
            return Task.FromResult((minW, maxW, minH, maxH));
        }
        static public int ComputeMaxW(int i, int j, int maxW)
        {
            if (j > maxW)
                maxW = j;
            return maxW;
        }
        static public int ComputeMinW(int i, int j, int minW)
        {
            if (j < minW)
                minW = j;
            return minW;
        }
        static public int ComputeMaxH(int i, int j, int maxH)
        {
            if (i > maxH)
                maxH = i;
            return maxH;
        }
        static public int ComputeMinH(int i, int j, int minH)
        {
            if (i < minH)
                minH = i;
            return minH;
        }



        public async Task<byte[]> Crop()
        {

            (int, int, int, int) bounderies = await Task.Run(ComputeBounderies);

            int widthBlackSpaces = bounderies.Item2 - bounderies.Item1 + 1;  
            int heightBlackSpaces = bounderies.Item4 - bounderies.Item3 + 1;

            Image<Rgba32> result = new Image<Rgba32>(widthBlackSpaces, heightBlackSpaces);

            int[,] resultMatrix = new int[heightBlackSpaces, widthBlackSpaces];

            for (int i = 0; i < heightBlackSpaces; i++)
            {
                for (int j = 0; j < widthBlackSpaces; j++)
                {
                    resultMatrix[i, j] = _imageMatrix[bounderies.Item3 + i, bounderies.Item1 + j];
                    if (resultMatrix[i, j] == 0)
                    {
                        result[j,i] = Color.FromRgb(255, 255, 255);
                    }
                    else
                    {
                        result[j, i] = Color.FromRgba(0, 0, 0, Convert.ToByte(resultMatrix[i, j]));
                    }
                }
            }


            var stream = new MemoryStream();
            result.SaveAsPng(stream);

            result.Dispose();
            return stream.ToArray();
        }

        private Image<Rgba32> ImageCreator(int splitIndex, int lastSplit, int[,] imageMatrix)
        {
            Image<Rgba32> temp = new Image<Rgba32>(splitIndex - lastSplit, image.Height);
            for (int j = 0; j < image.Height; j++)
                for (int i = lastSplit; i < splitIndex; i++)
                {
                    if (imageMatrix[j, i] == 0)
                    {
                        temp[i - lastSplit, j] = Color.FromRgb(255, 255, 255);
                    }
                    else
                    {
                        temp[i - lastSplit, j] = Color.FromRgba(0, 0, 0, Convert.ToByte(imageMatrix[j, i]));
                    }
                }
            return temp;
        }

        public async Task<List<byte[]>> Split()
        {
            List<byte[]> result = new List<byte[]>();
            int lastNotBank = -1;
            List<int> splitList = new List<int>();

            for (int j = 0; j < image.Width; j++)
            {
                bool isLineWhite = true;
                for (int i = 0; i < image.Height; i++)
                    if (_imageMatrix[i, j] == 255)
                    {
                        isLineWhite = false;
                        lastNotBank = 1;
                    }
                if (isLineWhite && lastNotBank != -1)
                {
                    splitList.Add(j);
                    lastNotBank = -1;
                }
            }

            int lastSplit = 0;

            foreach (int splitIndex in splitList)
            {
                Image<Rgba32> temp = await Task<Image<Rgba32>>.Run(() => ImageCreator(splitIndex, lastSplit, _imageMatrix));
                lastSplit = splitIndex;
                var stream = new MemoryStream();
                temp.SaveAsPng(stream);
                temp.Dispose();
                result.Add(stream.ToArray());

            }

            return result;
        }

        public async Task<IEnumerable<float>> GetFlattenedMatrix()
        {
            List<float> result = new List<float>();
            int[,] pixelMatrix = await Task.Run(GetPixelMatrixFromImage);
            foreach (int x in pixelMatrix)
            {
                result.Add(x);
            }
            return result;
        }


    }
}
