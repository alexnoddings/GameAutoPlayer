using System;
using System.Drawing;
using System.Drawing.Imaging;
using Metis.Core.Interaction;
using Metis.Core.Settings;
using SysProcess = System.Diagnostics.Process;

namespace Metis.Interaction.WindowCapture.Process
{
    public sealed class ProcessWindowCaptureHandler : IWindowCaptureHandler
    {
        private ISystemSettings SystemSettings { get; }

        public ProcessWindowCaptureHandler(ISystemSettings systemSettings)
        {
            SystemSettings = systemSettings;
        }

        /// <inheritdoc />
        public bool CanCaptureWindow => SystemSettings.TargetProcess?.HasExited == false;

        /// <inheritdoc />
        public Bitmap GetWindow()
        {
            if (!CanCaptureWindow)
                throw new InvalidOperationException("Unable to capture target process (target process not running)");
            SysProcess? process = SystemSettings.TargetProcess;
            IntPtr hwnd = process.MainWindowHandle;
            NativeMethods.GetWindowRect(hwnd, out Rect windowRect);
            var window = new Bitmap(windowRect.Width, windowRect.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(window))
            {
                IntPtr windowHdc = g.GetHdc();
                NativeMethods.PrintWindow(hwnd, windowHdc, 0);
                g.ReleaseHdc(windowHdc);
            }

            return window;
        }

    }
}
