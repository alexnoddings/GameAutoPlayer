using System.ComponentModel;
using System.Diagnostics;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Interaction;

namespace Metis.Core.Settings
{
    /// <summary>
    ///     The settings used by the main system.
    /// </summary>
    public interface ISystemSettings : INotifyPropertyChanged
    {
        /// <summary>
        ///     Gets the name of the process to target.
        /// </summary>
        public string TargetProcessName { get; }

        /// <summary>
        ///     The instance of the target process. Will be null if the process is not running.
        /// </summary>
        public Process? TargetProcess { get; }

        /// <summary>
        ///     If the target process is running.
        /// </summary>
        public bool IsTargetProcessRunning => TargetProcess?.HasExited == false;

        /// <summary>
        ///     An instance of the handler responsible for providing input to the target process.
        /// </summary>
        public IInputHandler InputHandlerInstance { get; }

        /// <summary>
        ///     An instance of the handler responsible for capturing the window of the target process.
        /// </summary>
        public IWindowCaptureHandler WindowCaptureHandlerInstance { get; }

        /// <summary>
        ///     An instance of the handler responsible for OCR.
        /// </summary>
        public IOcrHandler OcrHandlerInstance { get; }
    }
}
