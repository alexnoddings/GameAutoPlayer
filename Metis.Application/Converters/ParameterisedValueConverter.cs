using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace Metis.Application.Converters
{
    /// <summary>
    ///     Converts a value between <typeparamref name="TFrom"/> and <typeparamref name="TTo"/>. Conversion back is not guaranteed, and is up the the implementation.
    /// </summary>
    /// <typeparam name="TFrom">The type to convert from.</typeparam>
    /// <typeparam name="TTo">The type to convert to.</typeparam>
    /// <typeparam name="TParam">The type for the parameter.</typeparam>
    /// <inheritdoc />
    internal abstract class ParameterisedValueConverter<TFrom, TTo, TParam> : IValueConverter
    {
        /// <inheritdoc />
        [Obsolete("Use " + nameof(Convert) + " with typed value instead.", false)]
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                TFrom fromValue when (targetType == typeof(TTo) || targetType.IsSubclassOf(typeof(TTo))) && parameter is TParam param => Convert(fromValue, targetType, param, culture),
                null when parameter is TParam param => Convert(default!, targetType, param, culture),
                _ => throw new InvalidOperationException(
                    $"Expected source of type {typeof(TFrom).Name} with target of type {typeof(TTo).Name} and parameter of type {typeof(TParam).Name}, instead received {value?.GetType().Name}, {targetType?.Name}, and {parameter?.GetType().Name}")
            };
        }

        /// <summary>
        ///     Converts from <typeparamref name="TFrom"/> to <typeparamref name="TTo"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type to convert to.</param>
        /// <param name="param">The parameter passed to the converter.</param>
        /// <param name="culture">The culture to use for conversion.</param>
        /// <returns>The converted value.</returns>
        public abstract TTo Convert([MaybeNull] TFrom value, Type targetType, TParam param, CultureInfo culture);

        /// <inheritdoc />
        [Obsolete("Use " + nameof(Convert) + " with typed value instead.", false)]
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TTo fromValue && (targetType == typeof(TFrom) || targetType.IsSubclassOf(typeof(TFrom))) && parameter is TParam param)
                return ConvertBack(fromValue, targetType, param, culture);
            throw new InvalidOperationException($"Expected source of type {typeof(TTo).Name} with target of type {typeof(TFrom).Name} and parameter of type {typeof(TParam).Name}, instead received {targetType?.Name}, {value?.GetType().Name}, and {parameter?.GetType().Name}");
        }

        /// <summary>
        ///     Converts back from <typeparamref name="TTo"/> to <typeparamref name="TFrom"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type to convert to.</param>
        /// <param name="param">The parameter passed to the converter.</param>
        /// <param name="culture">The culture to use for conversion.</param>
        /// <returns>The value converted back.</returns>
        /// <remarks>Conversion back is optional. Not implementing this will throw a <see cref="NotImplementedException"/>.</remarks>
        public virtual TFrom ConvertBack([MaybeNull] TTo value, Type targetType, TParam param, CultureInfo culture)
        {
            // We opt to throw an exception rather than return default to fail early, rather than introduce hidden bugs
            throw new NotImplementedException($"Value converter {GetType().Name} does not support reverse conversion");
        }
    }
}
