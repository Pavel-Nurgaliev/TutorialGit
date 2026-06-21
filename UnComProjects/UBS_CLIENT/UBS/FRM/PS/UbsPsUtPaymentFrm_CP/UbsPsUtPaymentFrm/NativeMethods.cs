using System.Runtime.InteropServices;

namespace UbsBusiness
{
    /// <summary>
    /// P/Invoke replacement for the legacy <c>modWinAPI.bas</c> module:
    /// <c>POINTAPI</c> struct, <c>GetCursorPos</c>, and <c>Sleep</c>.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Screen coordinates of a point in pixels.
        /// VB6 equivalent: <c>POINTAPI</c> with <c>x</c>/<c>y</c> as Long.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// Retrieves the current cursor position in screen coordinates.
        /// </summary>
        /// <param name="lpPoint">Receives the cursor position.</param>
        /// <returns>Non-zero on success.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// Suspends execution of the current thread for the specified interval.
        /// </summary>
        /// <param name="dwMilliseconds">Sleep interval in milliseconds.</param>
        [DllImport("kernel32.dll")]
        public static extern void Sleep(uint dwMilliseconds);
    }
}
