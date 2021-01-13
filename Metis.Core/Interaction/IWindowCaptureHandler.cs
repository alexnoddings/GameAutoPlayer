using System.Drawing;

namespace Metis.Core.Interaction
{
    /// <summary>
    ///     Handles sending input to the process.
    /// </summary>
    public interface IWindowCaptureHandler
    {
        /// <summary>
        ///     Gets a value indicating whether the handler is able to capture the window.
        /// </summary>
        bool CanCaptureWindow { get; }

        /// <summary>
        ///     Gets an image of the process' main window.
        /// </summary>
        /// <returns>An image of the process' main window.</returns>
        Bitmap GetWindow();
    }
}
