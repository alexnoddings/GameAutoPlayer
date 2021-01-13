using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metis.Core.Interaction;
using Metis.Core.Settings;

namespace Metis.Interaction.Input.Desktop
{
    public sealed class DesktopInputHandler : IInputHandler
    {
        /// <inheritdoc />
        public Task SendKeyAsync(char character)
        {
            var charStr = character.ToString(CultureInfo.InvariantCulture);
            SendKeys.SendWait(charStr);
            return Task.CompletedTask;
        }

        private ISystemSettings SystemSettings { get; }

        public DesktopInputHandler(ISystemSettings systemSettings)
        {
            SystemSettings = systemSettings ?? throw new ArgumentNullException(nameof(systemSettings));
        }

        /// <inheritdoc />
        public bool CanSend => IsTargetProcessTheActiveWindow();

        private bool IsTargetProcessTheActiveWindow()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            string foregroundProcess = Process.GetProcessById((int)pid).ProcessName;

            // Check if current foreground window belongs to the target process
            return SystemSettings.TargetProcessName == foregroundProcess;
        }
    }
}
