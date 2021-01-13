using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metis.Application.Models
{
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Sets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backing">A reference to the backing store for the value.</param>
        /// <param name="value">The new value.</param>
        /// <param name="forceUpdate">Whether <see cref="PropertyChanged"/> should be invoked if the parameter's value was not changed.</param>
        /// <param name="propertyName">The name of the property. Will be auto-set by the compiler.</param>
        /// <remarks>
        ///     Acts as a helper which invokes <see cref="PropertyChanged"/> for you.
        ///     No event is invoked if the new value is the same as the existing value when <paramref name="forceUpdate" /> is <code>false</code>.
        /// </remarks>
        protected void SetProperty<T>(ref T backing, T value, bool forceUpdate = false, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backing, value) && !forceUpdate)
                return;
            backing = value;
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Notifies subscribers that a property was changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that was changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
