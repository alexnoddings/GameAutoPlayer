using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Metis.Interaction.WindowCapture.Process
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Element parameters should be documented", Justification = "Native methods are documented externally.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Element return value should be documented", Justification = "Native methods are documented externally.")]
    internal static class NativeMethods
    {

        /// <summary>See <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect" />.</summary>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        /// <summary>See <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-printwindow" />.</summary>
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
    }
}
