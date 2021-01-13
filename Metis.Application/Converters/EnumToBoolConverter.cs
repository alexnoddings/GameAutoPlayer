using System;
using System.Globalization;

namespace Metis.Application.Converters
{
    /// <summary>
    ///     Converts an enumerable to a boolean.
    /// </summary>
    internal class EnumToBoolConverter : ParameterisedValueConverter<Enum, bool?, string>
    {
        /// <inheritdoc />
        public override bool? Convert(Enum value, Type targetType, string param, CultureInfo culture)
        {
            return Enum.Parse(value.GetType(), param).Equals(value);
        }

        /// <inheritdoc />
        public override Enum ConvertBack(bool? value, Type targetType, string param, CultureInfo culture)
        {
            return (Enum) Enum.Parse(targetType, param);
        }
    }
}
