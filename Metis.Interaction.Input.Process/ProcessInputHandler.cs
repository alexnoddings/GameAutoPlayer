using System;
using System.Threading.Tasks;
using Metis.Core.Interaction;
using Metis.Core.Settings;
using SysProcess = System.Diagnostics.Process;

namespace Metis.Interaction.Input.Process
{
    public sealed class ProcessInputHandler : IInputHandler
    {
        private const uint MsgKeyDown = 0x0100;
        private const uint MsgKeyUp = 0x0101;
        private const uint MsgCharSent = 0x0102;

        private static readonly Random Rand = new Random();

        private ISystemSettings SystemSettings { get; }

        public ProcessInputHandler(ISystemSettings systemSettings)
        {
            SystemSettings = systemSettings ?? throw new ArgumentNullException(nameof(systemSettings));
        }

        /// <inheritdoc />
        public bool CanSend => SystemSettings.TargetProcess?.HasExited == false;

        /// <inheritdoc />
        public async Task SendKeyAsync(char character)
        {
            if (!CanSend)
                throw new InvalidOperationException("Unable to send input to target process (target process not running)");

            SysProcess? process = SystemSettings.TargetProcess!;
            NativeMethods.SendMessage(process, MsgKeyDown, character);
            NativeMethods.SendMessage(process, MsgCharSent, character);
            await Task.Delay(Rand.Next(25, 75));
            NativeMethods.SendMessage(process, MsgKeyUp, character);
        }
    }
}
