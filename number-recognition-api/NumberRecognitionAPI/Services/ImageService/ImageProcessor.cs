using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageService
{
    class ImageProcessor
    {
        private Bitmap image;
        public async Task<int[,]> Encode(byte[] source)
        {
            MemoryStream ms = new MemoryStream(source);
            image = new Bitmap(ms);
            int[,] result = await Task.Run(GetPixelMatrixFromBitmap);
            
            return result;
        }

        private Task<int[,]> GetPixelMatrixFromBitmap()
        {
            int[,] result = new int[image.Width, image.Height];
            StringBuilder lines = new StringBuilder();
            for (int i = 0; i < image.Width; i++)
            {
                lines.Append("[");
                string line = "[";
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    if (pixel.R == 0 || pixel.G == 0 || pixel.B == 0)
                    {
                        result[i, j] = pixel.A % 1000;
                        line = line + pixel.A % 1000;
                        lines.Append(pixel.A % 1000);
                    }
                    else
                    {
                        result[i, j] = 0;
                        lines.Append("0, ");
                    }

                }
                lines.Append("]\n");
            }
            File.WriteAllLines("temp.txt", lines.ToString().Split("\n"));
            return Task.FromResult(result);
        }
    }
}
