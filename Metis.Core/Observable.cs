using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Metis.Core
{
    /// <summary>
    ///     Provides a base implementation of an object who notifies observers when a property is changed.
    /// </summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     Sets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backing">A reference to the backing store for the value.</param>
        /// <param name="value">The new value.</param>
        /// <param name="forceUpdate">Whether <see cref="PropertyChanged"/> should be invoked if the parameter's value was not changed.</param>
        /// <param name="propertyName">The name of the property. Will be auto-set by the compiler.</param>
        /// <returns>True if the property was changed, otherwise false.</returns>
        /// <remarks>
        ///     Acts as a helper which invokes <see cref="PropertyChanged"/> for you.
        ///     No event is invoked if the new value is the same as the existing value when <paramref name="forceUpdate" /> is <code>false</code>.
        /// </remarks>
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Shorter name is useful and is highly unlikely to be confused.")]
        protected virtual bool Set<T>(ref T backing, T value, bool forceUpdate = false, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backing, value) && !forceUpdate)
                return false;
            backing = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Notifies subscribers that a property was changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that was changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
