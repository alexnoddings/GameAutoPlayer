using System.Drawing;

namespace Metis.Core.Extensions
{
    /// <summary>
    ///     Extensions for the <see cref="Rectangle"/> struct.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        ///     Scales a rectangle up or down by a factor.
        /// </summary>
        /// <param name="rectangle">The original rectangle.</param>
        /// <param name="scale">The scale to use. A scale of 1 would return the original, 0.5 would return half, and 2 would return double.</param>
        /// <returns>A copy of <paramref name="rectangle"/> scaled up or down by <paramref name="scale"/>.</returns>
        public static Rectangle Scale(this Rectangle rectangle, double scale) =>
            new Rectangle(
                (int)(rectangle.X * scale),
                (int)(rectangle.Y * scale),
                (int)(rectangle.Width * scale),
                (int)(rectangle.Height * scale));
    }
}
