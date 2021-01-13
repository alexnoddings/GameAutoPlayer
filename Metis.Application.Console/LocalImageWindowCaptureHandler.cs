using System.Drawing;
using Metis.Core.Interaction;

namespace Metis.Application.Console
{
    internal class LocalImageWindowCaptureHandler : IWindowCaptureHandler
    {
        private const string ImagePath = "./window.png";

        /// <inheritdoc />
        public bool Ready { get; } = true;

        /// <inheritdoc />
        public Bitmap GetWindow()
        {
            return new Bitmap(ImagePath);
        }
    }
}
