using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
