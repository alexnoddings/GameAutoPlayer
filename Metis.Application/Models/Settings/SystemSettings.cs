using System;
using Metis.Core.Settings;
using System.Diagnostics;
using System.Linq;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Interaction;
using Metis.ImageProcessing.Ocr.Tesseract;
using Metis.Interaction.Input.Desktop;
using Metis.Interaction.Input.Process;
using Metis.Interaction.WindowCapture.Desktop;
using Metis.Interaction.WindowCapture.Process;

namespace Metis.Application.Models
{
    internal class SystemSettings : SavedSettings, ISystemSettings
    {
        /// <inheritdoc />
        protected override string SettingsName { get; } = "System";

        private string _targetProcessName = string.Empty;
        [SavedSetting]
        public string TargetProcessName
        {
            get => _targetProcessName;
            set => Set(ref _targetProcessName, value);
        }

        public Process? TargetProcess => Process.GetProcessesByName(_targetProcessName).FirstOrDefault();

        private InputMethod _inputMethod = InputMethod.Process;
        [SavedSetting]
        public InputMethod InputMethod
        {
            get => _inputMethod;
            set
            {
                if (value == _inputMethod && InputHandlerInstance != null) return;
                Set(ref _inputMethod, value);
                switch (value)
                {
                    case InputMethod.Process:
                        (InputHandlerInstance as IDisposable)?.Dispose();
                        InputHandlerInstance = new ProcessInputHandler(this);
                        break;
                    case InputMethod.Desktop:
                        (InputHandlerInstance as IDisposable)?.Dispose();
                        InputHandlerInstance = new DesktopInputHandler(this);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private IInputHandler _inputHandlerInstance;
        public IInputHandler InputHandlerInstance
        {
            get => _inputHandlerInstance;
            private set => Set(ref _inputHandlerInstance, value);
        }

        private WindowCaptureMethod _windowCaptureMethod = WindowCaptureMethod.Process;
        [SavedSetting]
        public WindowCaptureMethod WindowCaptureMethod
        {
            get => _windowCaptureMethod;
            set
            {
                if (value == _windowCaptureMethod && WindowCaptureHandlerInstance != null) return;
                Set(ref _windowCaptureMethod, value);
                switch (value)
                {
                    case WindowCaptureMethod.Process:
                        (WindowCaptureHandlerInstance as IDisposable)?.Dispose();
                        WindowCaptureHandlerInstance = new ProcessWindowCaptureHandler(this);
                        break;
                    case WindowCaptureMethod.Desktop:
                        (WindowCaptureHandlerInstance as IDisposable)?.Dispose();
                        WindowCaptureHandlerInstance = new DesktopWindowCaptureHandler(this);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private IWindowCaptureHandler _windowCaptureHandlerInstance;
        public IWindowCaptureHandler WindowCaptureHandlerInstance
        {
            get => _windowCaptureHandlerInstance;
            private set => Set(ref _windowCaptureHandlerInstance, value);
        }

        private IOcrHandler _ocrHandlerInstance;
        public IOcrHandler OcrHandlerInstance
        {
            get => _ocrHandlerInstance;
            private set => Set(ref _ocrHandlerInstance, value);
        }

        private bool _isAlwaysOnTop = true;
        [SavedSetting]
        public bool IsAlwaysOnTop
        {
            get => _isAlwaysOnTop;
            set => Set(ref _isAlwaysOnTop, value);
        }

        public SystemSettings()
        {
            // Ensure that instances are created properly
            InputMethod = _inputMethod;
            WindowCaptureMethod = _windowCaptureMethod;
            OcrHandlerInstance = new TesseractOcrHandler();
        }
    }
}
