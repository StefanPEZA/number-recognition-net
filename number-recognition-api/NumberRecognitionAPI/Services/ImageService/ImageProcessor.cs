using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Services.ImageService
{
    class ImageProcessor
    {
         async public Task encode(byte[] source)
        {

            MemoryStream ms = new MemoryStream(source);
            Bitmap image = new Bitmap(ms);
            using (FileStream fs = File.Create("temp.txt")) ;
            int[,] result = new int[ image.Width,image.Height];

            string[] lines = new string[image.Width];
            for (int i = 0; i < image.Width; i++)
            {
                string line = "[";
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    if (pixel.R == 0 || pixel.G == 0 || pixel.B == 0)
                    {
                        result[i, j] = pixel.A % 1000;
                        line = line + pixel.A%1000;
                    }
                    else
                    {
                        result[i, j] = 0;
                        line = line + "0, ";
                    }
 
                }
                line += "]";
                lines[i] = line;
            }
            File.WriteAllLines("temp.txt", lines);
        }
    }
}
