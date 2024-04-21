using ImageQT.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQT.DllInterop.Windows;
internal static partial class User32
{
    private const string _dllName = "User32.dll";

    [DllImport(_dllName, SetLastError = true)]
    public static extern ushort RegisterClassExW([In] ref WndClassExW unnamedParam1);

    [DllImport(_dllName, SetLastError = true)]
    public static extern WindowWrapper CreateWindowExW(IntPtr dwExStyle,
        [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
        [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
        IntPtr dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam
    );

    [LibraryImport(_dllName)]
    public static partial int ShowWindow(WindowWrapper hwnd, int nCmdShow);

    [LibraryImport(_dllName, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DestroyWindow(IntPtr hWnd);

    [LibraryImport(_dllName, SetLastError = true)]
    public static partial IntPtr DefWindowProcW(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [LibraryImport(_dllName, SetLastError = true)]
    public static partial void PostQuitMessage(int exitCode);

    [DllImport(_dllName)]
    public static extern IntPtr DispatchMessage([In] ref LpMsg lpMsg);

    [DllImport(_dllName)]
    public static extern int GetMessage(out LpMsg lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [LibraryImport(_dllName)]
    public static partial IntPtr GetDC(IntPtr hWnd);

    [LibraryImport(_dllName)]
    public static partial int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [LibraryImport(_dllName)]
    public static partial int UnregisterClassW([MarshalAs(UnmanagedType.LPWStr)] string hwnd, IntPtr hInstance);
}
