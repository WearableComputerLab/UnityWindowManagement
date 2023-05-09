using System;
using System.Runtime.InteropServices;

public static class WinAPI
{
    // Function to get the handle for the foreground window
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    // Function to get the dimensions of a window given its handle
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    // Function to convert screen coordinates to client coordinates for a specific window
    [DllImport("user32.dll")]
    public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    // Function to send a Windows message to a window (used here for sending click messages)
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

    // Windows message codes for left button down and up, representing a click
    public const UInt32 WM_LBUTTONDOWN = 0x0201;
    public const UInt32 WM_LBUTTONUP = 0x0202;

    // Structs representing a point (for coordinates) and a rectangle (for window dimensions)
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
