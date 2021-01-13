using System.Drawing;

namespace Metis.Core.Extensions
{
    /// <summary>
    ///     Extensions for the <see cref="Color"/> struct.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        ///     Whether a colour is within the range of two other colours.
        /// </summary>
        /// <param name="colour">The colour to test.</param>
        /// <param name="lower">The lower bound colour.</param>
        /// <param name="upper">The upper bound colour.</param>
        /// <returns>If the colour is within range.</returns>
        public static bool IsInRange(this Color colour, Color lower, Color upper)
        {
            return lower.R <= colour.R && colour.R <= upper.R &&
                   lower.G <= colour.G && colour.G <= upper.G &&
                   lower.B <= colour.B && colour.B <= upper.B;
        }
    }
}
