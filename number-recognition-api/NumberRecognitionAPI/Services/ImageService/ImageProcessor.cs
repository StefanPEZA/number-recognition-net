using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageService
{
    class ImageProcessor
    {
        private Bitmap image;
        private string imageMatrix;

        public async Task<byte[,]> Encode(byte[] source)
        {
            MemoryStream ms = new MemoryStream(source);
            image = new Bitmap(ms);
            byte[,] result = await Task.Run(GetPixelMatrixFromBitmap);
            
            return result;
        }

        private Task<byte[,]> GetPixelMatrixFromBitmap()
        {
            byte[,] result = new byte[image.Width, image.Height];
            StringBuilder lines = new StringBuilder();
            for (int i = 0; i < image.Width; i++)
            {
                lines.Append('[');
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    if (pixel.R == 0 || pixel.G == 0 || pixel.B == 0)
                    {
                        result[i, j] = pixel.A;
                        lines.Append(pixel.A);
                    }
                    else
                    {
                        result[i, j] = 0;
                        lines.Append("0, ");
                    }

                }
                lines.Append("]\n");
            }
            imageMatrix = lines.ToString();
            File.WriteAllLines("temp.txt", imageMatrix.Split("\n"));
            return Task.FromResult(result);
        }

        public override string ToString()
        {
            return imageMatrix;
        }
    }
}
