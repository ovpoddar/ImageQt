#if DEBUG || Windows
using System.Runtime.InteropServices;

namespace ImageQT.Models.Windows;

[StructLayout(LayoutKind.Sequential)]
internal struct LpMsg
{
    public nint hWnd;
    public uint msg;
    public nint wParam;
    public nint lParam;
    public uint time;
    public Point pt;
}
#endif
