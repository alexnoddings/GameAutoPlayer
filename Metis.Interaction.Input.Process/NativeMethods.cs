using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using SysProcess = System.Diagnostics.Process;

namespace Metis.Interaction.Input.Process
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Element parameters should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Native methods are documented externally.")]
    internal static class NativeMethods
    {
        /// <summary>See <see cref="SendMessage(IntPtr,uint,IntPtr,IntPtr)" />.</summary>
        public static void SendMessage(SysProcess process, uint message, char character)
        {
            SendMessage(process.MainWindowHandle, message, (IntPtr)character, IntPtr.Zero);
        }

        /// <summary>See <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-printwindow" />.</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}
