using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Metis.Interaction.Input.Desktop
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Element parameters should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Native methods are documented externally.")]
    internal static class NativeMethods
    {
        /// <summary>See <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid" />.</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        /// <summary>See <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow" />.</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
