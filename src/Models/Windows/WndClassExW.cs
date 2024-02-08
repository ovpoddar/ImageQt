using System.Runtime.InteropServices;

namespace ImageQt.Models.Windows;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct WndClassExW
{
    public required uint ClassSize;
    public required WindowStyle style;
    public required IntPtr lpfnWndProc;
    public required int cbClsExtra;
    public required int cbWndExtra;
    public required IntPtr hInstance;
    public required IntPtr hIcon;
    public required IntPtr hCursor;
    public required IntPtr hbrBackground;
    [MarshalAs(UnmanagedType.LPWStr)]
    public required string lpszMenuName;
    [MarshalAs(UnmanagedType.LPWStr)]
    public required string lpszClassName;
    public required IntPtr hIconSm;
}
