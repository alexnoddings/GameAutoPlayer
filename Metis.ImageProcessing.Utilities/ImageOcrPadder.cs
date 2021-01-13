using System;
using System.Drawing;

namespace Metis.ImageProcessing.Utilities
{
    /// <summary>
    ///     Handles padding an image for OCR-ing.
    ///     Most OCR engines are designed to process larger bodies of text; as such trying to process small texts (e.g. 1/2 words) can fail.
    ///     Padding the image with known text can vastly improve accuracy.
    /// </summary>
    public class ImageOcrPadder: IDisposable
    {
        private readonly int _targetHeight;
        private Bitmap _left;
        private Bitmap _right;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImageOcrPadder"/> class.
        /// </summary>
        /// <param name="left">The left image to pad with.</param>
        /// <param name="right">The right image to pad with.</param>
        public ImageOcrPadder(Bitmap left, Bitmap right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));

            if (_left.Height != _right.Height)
                throw new ArgumentException("Left and right images must be of the same height");
            _targetHeight = _left.Height;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImageOcrPadder"/> class.
        /// </summary>
        /// <param name="leftPath">The path of left image to pad with.</param>
        /// <param name="rightPath">The path of right image to pad with.</param>
        public ImageOcrPadder(string leftPath, string rightPath)
            : this(new Bitmap(leftPath), new Bitmap(rightPath))
        {
        }

        /// <summary>
        ///     Pads an image using the left and right pad.
        /// </summary>
        /// <param name="centre">The center image to scale and pad.</param>
        /// <returns>An image padded with the left and right.</returns>
        public Bitmap Pad(Bitmap centre)
        {
            double scale = _targetHeight / (double)centre.Height;
            var targetWidth = (int) (centre.Width * scale);
            var padded = new Bitmap(_left.Width + targetWidth + _right.Width, _targetHeight);

            using var graphics = Graphics.FromImage(padded);
            graphics.DrawImage(_left, new Point(0, 0));
            graphics.DrawImage(centre, new Rectangle(_left.Width, 0, targetWidth, _targetHeight), new Rectangle(0, 0, centre.Width, centre.Height), GraphicsUnit.Pixel);
            graphics.DrawImage(_right, new Point(_left.Width + targetWidth, 0));

            padded.Save("paddedimg.png");
            return padded;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _left?.Dispose();
            _left = null;
            _right?.Dispose();
            _right = null;
        }
    }
}
