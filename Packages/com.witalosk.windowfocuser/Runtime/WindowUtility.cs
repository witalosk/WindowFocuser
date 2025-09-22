using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowFocuser
{
    public static class WindowUtility
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int SW_RESTORE = 9;

        /// <summary>
        /// Focus the window of the specified process.
        /// </summary>
        /// <param name="processName">The name of the process to focus. </param>
        /// <returns>True if the window was focused successfully, false otherwise. </returns>
        [Obsolete("Use FocusWindowByProcessName instead.")]
        public static bool FocusWindow(string processName)
        {
            return FocusWindowByProcessName(processName);
        }

        /// <summary>
        /// Focus the window of the specified process.
        /// </summary>
        /// <param name="processName">The name of the process to focus. </param>
        /// <returns>True if the window was focused successfully, false otherwise. </returns>
        public static bool FocusWindowByProcessName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length <= 0) return false;

            IntPtr hWnd = processes[0].MainWindowHandle;
            if (hWnd == IntPtr.Zero) return false;

            ActivateWindow(hWnd);
            return true;
        }

        /// <summary>
        /// Focus the window by its title.
        /// </summary>
        public static bool FocusWindowByWindowName(string windowName)
        {
            IntPtr hWnd = FindWindow(null, windowName);

            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            ActivateWindow(hWnd);
            return true;
        }

        /// <summary>
        /// Activate the specified window.
        /// </summary>
        private static void ActivateWindow(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }

            SetForegroundWindow(hWnd);
        }
    }
}