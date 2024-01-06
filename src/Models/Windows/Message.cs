using System.Drawing;
using System.Runtime.InteropServices;

namespace ImageQt.Models.Windows;
[StructLayout(LayoutKind.Sequential)]
internal struct Message
{
    public nint hWnd;
    public uint msg;
    public nint wParam;
    public nint lParam;
    public uint time;
    public Point pt;
}
