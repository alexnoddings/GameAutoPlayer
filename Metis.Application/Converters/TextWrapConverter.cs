using System;
using System.Globalization;

namespace Metis.Application.Converters
{
    internal class TextWrapConverter : ParameterisedValueConverter<string, string, string>
    {
        /// <inheritdoc />
        public override string Convert(string value, Type targetType, string param, CultureInfo culture) =>
            param.Replace("%s", value);
    }
}
