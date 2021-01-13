using System.Drawing;

namespace Metis.ImageProcessing.Utilities
{
    public static class ColourAverager
    {
        public static Color GetAverageColourFromImage(Bitmap image)
        {
            var red = 0;
            var green = 0;
            var blue = 0;

            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                Color pixel = image.GetPixel(x, y);
                red += pixel.R;
                green += pixel.G;
                blue += pixel.B;
            }

            int imagePixels = image.Width * image.Height;

            return Color.FromArgb(red / imagePixels, green / imagePixels, blue / imagePixels);
        }
    }
}
