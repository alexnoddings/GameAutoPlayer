using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Metis.Core.Interaction;
using Metis.Core.Settings;

namespace Metis.Interaction.WindowCapture.Desktop
{
    public sealed class DesktopWindowCaptureHandler : IWindowCaptureHandler
    {
        private ISystemSettings SystemSettings { get; }

        public DesktopWindowCaptureHandler(ISystemSettings systemSettings)
        {
            SystemSettings = systemSettings;
        }

        public Bitmap GetWindow()
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            var desktop = new Bitmap(screenBounds.Width, screenBounds.Height);

            using (Graphics graphics = Graphics.FromImage(desktop))
            {
                graphics.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, desktop.Size, CopyPixelOperation.SourceCopy);
            }

            return desktop;
        }

        /// <inheritdoc />
        public bool CanCaptureWindow => IsTargetProcessTheActiveWindow();

        private bool IsTargetProcessTheActiveWindow()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            string foregroundProcess = Process.GetProcessById((int) pid).ProcessName;

            // Check if current foreground window belongs to the target process
            return SystemSettings.TargetProcessName == foregroundProcess;
        }
    }
}
