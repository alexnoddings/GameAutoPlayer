using System;
using System.Globalization;

namespace Metis.Application.Converters
{
    /// <summary>
    ///     Inverts a boolean value.
    /// </summary>
    internal class BoolInverterConverter : ValueConverter<bool, bool>
    {
        /// <inheritdoc />
        public override bool Convert(bool value, Type targetType, CultureInfo culture) => !value;

        /// <inheritdoc />
        public override bool ConvertBack(bool value, Type targetType, CultureInfo culture) => !value;
    }
}
