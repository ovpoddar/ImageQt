#if DEBUG || Windows
using System.Runtime.InteropServices;

namespace ImageQT.Models.Windows;
internal struct WndClassExW
{
    public required uint ClassSize;
    public required WindowStyle style;
    public required IntPtr lpFnWndProc;
    public required int cbClsExtra;
    public required int cbWndExtra;
    public required IntPtr hInstance;
    public required IntPtr hIcon;
    public required IntPtr hCursor;
    public required IntPtr hBrBackground;
    [MarshalAs(UnmanagedType.LPWStr)]
    public required string lpszMenuName;
    [MarshalAs(UnmanagedType.LPWStr)]
    public required string lpszClassName;
    public required IntPtr hIconSm;
}
#endif