using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

