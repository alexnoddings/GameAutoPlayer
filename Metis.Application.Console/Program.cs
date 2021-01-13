using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Metis.Core.Display;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Interaction;
using Metis.Core.Settings;
using Metis.ImageProcessing.Ocr.Tesseract;
using Metis.Interaction.Input.Desktop;
using Metis.Modules.TypingGame;

namespace Metis.Application.Console
{
    public static class Program
    {
        private class SystemSettings : ISystemSettings
        {
            /// <inheritdoc />
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <inheritdoc />
            public string TargetProcessName { get; set; }

            /// <inheritdoc />
            public Process? TargetProcess { get; set; }

            /// <inheritdoc />
            public IInputHandler InputHandlerInstance { get; set; }

            /// <inheritdoc />
            public IWindowCaptureHandler WindowCaptureHandlerInstance { get; set; }

            /// <inheritdoc />
            public IOcrHandler OcrHandlerInstance { get; set; }
        }

        private class DisplayHelper : IDisplayHelper
        {
            /// <inheritdoc />
            public double DisplayWidth { get; set; }

            /// <inheritdoc />
            public double DisplayHeight { get; set; }

            /// <inheritdoc />
            public double DisplayCenterX { get; set; }

            /// <inheritdoc />
            public double DisplayCenterY { get; set; }

            /// <inheritdoc />
            public IOutlineArea CreateOutlineArea() => throw new System.NotImplementedException();
        }

        public static async Task Main()
        {
            SystemSettings systemSettings = new SystemSettings { WindowCaptureHandlerInstance = new LocalImageWindowCaptureHandler(), OcrHandlerInstance = new TesseractOcrHandler() };
            systemSettings.InputHandlerInstance = new DesktopInputHandler(systemSettings);
            IDisplayHelper displayHelper = new DisplayHelper { DisplayWidth = 2560, DisplayHeight = 1440 };
            var module = new TypingGameModule(systemSettings, displayHelper);
            var cts = new CancellationTokenSource();
            await module.RunAsync(cts.Token);
        }
    }
}