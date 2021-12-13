﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageService
{
    class ImageProcessor
    {
        private readonly Bitmap image;
        private string ImageMatrix { get; set; }

        public ImageProcessor(byte[] source)
        {
            MemoryStream ms = new MemoryStream(source);
            image = new Bitmap(ms);
        }

        private Task<int[,]> GetPixelMatrixFromBitmap()
        {
            int[,] result = new int[image.Height, image.Width];
            StringBuilder lines = new StringBuilder();
            for (int i = 0; i < image.Height; i++)
            {
                lines.Append('[');
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    if (pixel.R == 0 || pixel.G == 0 || pixel.B == 0)
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

        private Bitmap FillToAspectRatio(int width, int height)
        {
            double ratioH = Math.Max((double)width / image.Width, (double)image.Width / width);
            double ratioW = Math.Max((double)height / image.Height, (double)image.Height / height);
            Size newSize = new Size((int)(width * ratioW) + (image.Width / 2), (int)(height * ratioH) + (image.Height / 2));
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.FillRectangle(Brushes.White, 0, 0, newSize.Width, newSize.Height);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(image, (newSize.Width / 2) - (image.Width / 2), (newSize.Height / 2) - (image.Height / 2), image.Width, image.Height);
            }
            return newImage;
        }

        public async Task<byte[]> Resize(int width, int height)
        {
            Bitmap newImage = await Task.Run(() => FillToAspectRatio(width, height));
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(newImage, 0, 0, width, height);
            }

            var stream = new MemoryStream();
            result.Save(stream, ImageFormat.Png);

            newImage.Dispose();
            result.Dispose();

            //result.Save("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img.png", ImageFormat.Png);
            return stream.ToArray();
        }


        public async Task<byte[]> Crop()
        {
            int[,] imageMatrix = await Task.Run(GetPixelMatrixFromBitmap);

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
            // int widthWhiteSpaces = image.Width - widthBlackSpaces;
            // int heightWhiteSpaces = image.Width - widthBlackSpaces;

            Bitmap result = new Bitmap(widthBlackSpaces, heightBlackSpaces);

            int[,] resultMatrix = new int[heightBlackSpaces, widthBlackSpaces];

            for (int i = 0; i < heightBlackSpaces; i++)
            {
                for (int j = 0; j < widthBlackSpaces; j++)
                {
                    resultMatrix[i, j] = imageMatrix[minH + i, minW + j];
                    if (resultMatrix[i, j] == 0)
                    {
                        result.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        result.SetPixel(j, i, Color.FromArgb(resultMatrix[i, j], 0, 0, 0));
                    }
                }
            }

            //result.Save("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img.png", ImageFormat.Png);

            var stream = new MemoryStream();
            result.Save(stream, ImageFormat.Png);

            result.Dispose();
            return stream.ToArray();
        }

        private Bitmap BitmapCreator(int splitIndex, int lastSplit, int[,] imageMatrix)
        {
            Bitmap temp = new Bitmap(splitIndex - lastSplit, image.Height);
            for (int j = 0; j < image.Height; j++)
                for (int i = lastSplit; i < splitIndex; i++)
                {
                    if (imageMatrix[j, i] == 0)
                    {
                        temp.SetPixel(i - lastSplit, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        temp.SetPixel(i - lastSplit, j, Color.FromArgb(imageMatrix[j, i], 0, 0, 0));
                    }
                }
            return temp;
        }

        public async Task<List<byte[]>> Split()
        {
            List<byte[]> result = new List<byte[]>();
            int[,] imageMatrix = await Task.Run(GetPixelMatrixFromBitmap);
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
            //splitList.Remove(splitList[splitList.Count - 1]);

            foreach (int splitIndex in splitList)
            {
                Bitmap temp = BitmapCreator(splitIndex, lastSplit, imageMatrix);
                lastSplit = splitIndex;
                //temp.Save("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img" + k + ".png", ImageFormat.Png);
                var stream = new MemoryStream();
                temp.Save(stream, ImageFormat.Png);
                temp.Dispose();
                result.Add(stream.ToArray());

            }

            return result;
        }

        public async Task<IEnumerable<float>> GetFlattenedMatrix()
        {
            List<float> result = new List<float>();
            int[,] pixelMatrix = await Task.Run(GetPixelMatrixFromImage);
            foreach(int x in pixelMatrix)
            {
                result.Add(x);
            }
            return result;
        }


        public override string ToString()
        {
            return ImageMatrix;
        }
    }
}
