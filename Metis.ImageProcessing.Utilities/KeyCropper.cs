using System;
using System.Drawing;

namespace Metis.ImageProcessing.Utilities
{
    public static class KeyCropper
    {
        public static Bitmap? GetKeyImageFromWindow(Bitmap image)
        {
            Rectangle? keyRectangleNullable = GetKeyRectangleFromWindow(image);
            if (keyRectangleNullable == null) return null;
            Rectangle keyRectangle = keyRectangleNullable.Value;

            var cropped = new Bitmap(keyRectangle.Width, keyRectangle.Height);
            using Graphics g = Graphics.FromImage(cropped);
            var destRect = new Rectangle(0, 0, keyRectangle.Width, keyRectangle.Height);
            g.DrawImage(image, destRect, keyRectangle, GraphicsUnit.Pixel);

            return cropped;

        }

        private static Rectangle? GetKeyRectangleFromWindow(Bitmap image)
        {
            // The key circles around within a designated area. This makes finding it tricky; a better method would be good.

            int keyWidth;
            int keyHeight;

            int centerPixelR;
            int upPixelR;
            int rightPixelR;
            int downPixelR;
            int leftPixelR;

            switch (image.Width)
            {
                // Key image scales seemingly non-linearly based on resolution, meaning boundaries must be pre-set.
                // The colour of the top left pixels also change based on image scaling.
                case 2560 when image.Height == 1440:
                case 2576 when image.Height == 1479:
                    keyWidth = 38;
                    keyHeight = 48;

                    centerPixelR = 218;
                    upPixelR = 238;
                    rightPixelR = 211;
                    downPixelR = 178;
                    leftPixelR = 210;
                    break;
                case 1920 when image.Height == 1080:
                    keyWidth = 26;
                    keyHeight = 33;

                    centerPixelR = 241;
                    upPixelR = 234;
                    rightPixelR = 234;
                    downPixelR = 184;
                    leftPixelR = 196;
                    break;
                default:
                    throw new InvalidOperationException($"Key width and height unknown for windows of size {image.Width}x{image.Height}");
            }

            // Key image will only appear within a portion of the screen, don't bother searching outside of it
            var searchAreaX = (int)(image.Width * 0.25);
            var searchAreaY = (int)(image.Height * 0.4);
            var searchAreaW = (int)(image.Width * 0.12);
            var searchAreaH = (int)(image.Height * 0.2);

            // return new Rectangle(searchAreaX, searchAreaY, searchAreaW, searchAreaH);

            for (int x = searchAreaX; x < searchAreaX + searchAreaW; x++)
                for (int y = searchAreaY; y < searchAreaY + searchAreaH; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    // Check if the pixel could be the center pixel
                    if (pixel.R != centerPixelR) continue;

                    // If so, check the surrounding pixels as the background pulses colours
                    Color up = image.GetPixel(x, y - 1);
                    Color right = image.GetPixel(x + 1, y);
                    Color down = image.GetPixel(x, y + 1);
                    Color left = image.GetPixel(x - 1, y);
                    if (up.R != upPixelR || right.R != rightPixelR || down.R != downPixelR || left.R != leftPixelR) continue;

                    var keyRectangle = new Rectangle(x + 1, y + 1, keyWidth, keyHeight);
                    return keyRectangle;
                }

            return null;
        }
    }
}
