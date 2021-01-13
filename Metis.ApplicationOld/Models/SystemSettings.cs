using Metis.Application.Core.Interaction.WindowCapture;
using Metis.Core.Interaction.Input;

namespace Metis.Application.Models
{
    internal class SystemSettings : Observable
    {
        private string _processName;
        public string ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }

        private IInputHandler _inputHandler;
        public IInputHandler InputHandler
        {
            get => _inputHandler;
            set => SetProperty(ref _inputHandler, value);
        }

        private IWindowCaptureHandler _windowCaptureHandler;
        public IWindowCaptureHandler WindowCaptureHandler
        {
            get => _windowCaptureHandler;
            set => SetProperty(ref _windowCaptureHandler, value);
        }

        public SystemSettings()
        {
            ProcessName = "test!";
        }
    }
}
