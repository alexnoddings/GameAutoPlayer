using System;
using System.Globalization;
using System.Text;

namespace Metis.Application.Converters
{
    /// <summary>
    ///     Converts an enumerable to a human-readable string.
    /// </summary>
    internal class EnumToReadableStringConverter : ValueConverter<Enum, object>
    {
        /// <inheritdoc />
        public override object Convert(Enum value, Type targetType, CultureInfo culture)
        {
            var sb = new StringBuilder();
            foreach (char c in value.ToString())
            {
                if ('A' <= c && c <= 'Z')
                    sb.Append(" ");
                sb.Append(c);
            }

            return sb.ToString().Trim();
        }

        /// <inheritdoc />
        public override Enum ConvertBack(object value, Type targetType, CultureInfo culture)
        {
            if (value is string valueStr)
                return (Enum)Enum.Parse(targetType, valueStr.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase));
            throw new InvalidOperationException($"Expecting string value, received {value.GetType().Name}");
        }
    }
}
