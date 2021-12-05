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
        private Bitmap image;
        private string imageMatrix;

        public ImageProcessor(byte[] source)
        {
            MemoryStream ms = new MemoryStream(source);
            image = new Bitmap(ms);

        }

        private Task<int[,]> GetPixelMatrixFromBitmap()
        {
            int[,] result = new int[image.Height,image.Width];
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
            imageMatrix = lines.ToString();
            File.WriteAllLines("temp.txt", imageMatrix.Split("\n"));
            return Task.FromResult(result);
        }

        public async Task<byte[]> Resize(int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, 0, 0, width, height);
            }


            var stream = new MemoryStream();
            result.Save(stream, ImageFormat.Png);

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
            return stream.ToArray();
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
                    if(imageMatrix[i,j] == 255)
                    {
                        isLineWhite = false;
                        lastNotBank = 1;
                    }
                if(isLineWhite && lastNotBank != -1)
                {
                    splitList.Add(j);
                    lastNotBank = -1;
                }
            }

            int lastSplit = 0;
            //splitList.Remove(splitList[splitList.Count - 1]);

            foreach(int k in splitList){
                Bitmap temp = new Bitmap(k - lastSplit, image.Height);
                for (int j = 0;j<image.Height;j++)
                    for (int i = lastSplit; i < k; i++)
                    {
                        if (imageMatrix[j, i] == 0)
                        {
                            temp.SetPixel(i-lastSplit, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            temp.SetPixel(i-lastSplit, j, Color.FromArgb(imageMatrix[j, i], 0, 0, 0));
                        }
                    }
                lastSplit = k;
                temp.Save("C:\\Users\\ghiuz\\OneDrive\\Desktop\\img" + k + ".png", ImageFormat.Png);
                var stream = new MemoryStream();
                temp.Save(stream, ImageFormat.Png);
                result.Add(stream.ToArray());

            }



            return result;
        }


        public override string ToString()
        {
            return imageMatrix;
        }
    }
}
