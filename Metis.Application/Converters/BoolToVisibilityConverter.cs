using System;
using System.Globalization;
using System.Windows;

namespace Metis.Application.Converters
{
    /// <summary>
    ///     Converts an enumerable to a boolean.
    /// </summary>
    internal class BoolToVisibilityConverter : ParameterisedValueConverter<bool, Visibility, bool>
    {
        /// <inheritdoc />
        public override Visibility Convert(bool value, Type targetType, bool param, CultureInfo culture)
        {
            value = param ? !value : value;
            return value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <inheritdoc />
        public override bool ConvertBack(Visibility value, Type targetType, bool param, CultureInfo culture)
        {
            var result = value == Visibility.Visible;
            return param ? !result : result;
        }
    }
}
