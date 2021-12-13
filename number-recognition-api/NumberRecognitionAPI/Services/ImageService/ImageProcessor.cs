using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
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
        private string ImageMatrix { get; set; }

        public ImageProcessor(byte[] source)
        {
            image = Image.Load<Rgba32>(source);
        }

        private Task<int[,]> GetPixelMatrixFromImage()
        {
            int[,] result = new int[image.Height, image.Width];
            StringBuilder lines = new StringBuilder();
            for (int i = 0; i < image.Height; i++)
            {
                lines.Append('[');
                for (int j = 0; j < image.Width; j++)
                {
                    
                    Rgba32 pixel = image[j, i];
                    if (pixel.R <= 45 || pixel.G <= 45 || pixel.B <= 45)
                    {
                        result[i, j] = pixel.A;
                        lines.Append(pixel.A.ToString() + ",");
                    }
                    else
                    {
                        result[i, j] = 0;
                        lines.Append("0, ");
                    }

                }
                lines.Append("],\n");
            }
            ImageMatrix = lines.ToString();
            File.WriteAllLines("temp.txt", ImageMatrix.Split("\n"));
            return Task.FromResult(result);
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
            
            //result.SaveAsPng("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img.png");
            return stream.ToArray();
        }


        public async Task<byte[]> Crop()
        {
            int[,] imageMatrix = await Task.Run(GetPixelMatrixFromImage);

            int minW = image.Width;
            int maxW = 0;

            int minH = image.Height;
            int maxH = 0;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (imageMatrix[i, j] == 255 && j > maxW)
                        maxW = j;
                    if (imageMatrix[i, j] == 255 && j < minW)
                        minW = j;
                    if (imageMatrix[i, j] == 255 && i > maxH)
                        maxH = i;
                    if (imageMatrix[i, j] == 255 && i < minH)
                        minH = i;
                }
            }

            int widthBlackSpaces = maxW - minW + 1;
            int heightBlackSpaces = maxH - minH + 1;

            Image<Rgba32> result = new Image<Rgba32>(widthBlackSpaces, heightBlackSpaces);

            int[,] resultMatrix = new int[heightBlackSpaces, widthBlackSpaces];

            for (int i = 0; i < heightBlackSpaces; i++)
            {
                for (int j = 0; j < widthBlackSpaces; j++)
                {
                    resultMatrix[i, j] = imageMatrix[minH + i, minW + j];
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

            //result.SaveAsPng("C:\\Users\\gigib\\OneDrive\\Desktop\\img.png");

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
            int[,] imageMatrix = await Task.Run(GetPixelMatrixFromImage);
            int lastNotBank = -1;
            List<int> splitList = new List<int>();

            for (int j = 0; j < image.Width; j++)
            {
                bool isLineWhite = true;
                for (int i = 0; i < image.Height; i++)
                    if (imageMatrix[i, j] == 255)
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
                Image<Rgba32> temp = ImageCreator(splitIndex, lastSplit, imageMatrix);
                lastSplit = splitIndex;
                // temp.SaveAsPng("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img" + k ".png");
                var stream = new MemoryStream();
                temp.SaveAsPng(stream);
                temp.Dispose();
                result.Add(stream.ToArray());

            }

            return result;
        }


        public override string ToString()
        {
            return ImageMatrix;
        }
    }
}
