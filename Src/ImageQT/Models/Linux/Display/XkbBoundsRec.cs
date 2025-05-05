using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbBoundsRec
{
    public short X1;
    public short Y1;
    public short X2;
    public short Y2;
}