using ImageQt.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Windows;
internal static partial class Win
{
    private const string UserNativeDll = "user32.dll";

    [DllImport(UserNativeDll, SetLastError = true)]
    public static extern ushort RegisterClassExW([In] ref WndClassExW lpWndClass);

    [LibraryImport(UserNativeDll, SetLastError = true)]
    public static partial nint CreateWindowExW(uint dwExStyle,
        [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
        [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
        uint dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        nint hWndParent,
        nint hMenu,
        nint hInstance,
        nint lpParam
    );

    [LibraryImport(UserNativeDll)]
    public static partial int ShowWindow(nint hwnd, int nCmdShow);

    [LibraryImport(UserNativeDll, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DestroyWindow(nint hWnd);

    [LibraryImport(UserNativeDll, SetLastError = true)]
    public static partial nint DefWindowProcW(nint hWnd, uint msg, nint wParam, nint lParam);

    [LibraryImport(UserNativeDll, SetLastError = true)]
    public static partial void PostQuitMessage(int exitcode);

    [DllImport(UserNativeDll)]
    public static extern bool TranslateMessage([In] ref Message lpMsg);

    [DllImport(UserNativeDll)]
    public static extern nint DispatchMessage([In] ref Message lpmsg);

    [DllImport(UserNativeDll)]
    public static extern int GetMessage(out Message lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [LibraryImport(UserNativeDll)]
    public static partial nint GetDC(nint hWnd);

    [LibraryImport(UserNativeDll)]
    public static partial int ReleaseDC(nint hwnd, nint hdc);

}
