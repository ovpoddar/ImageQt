using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbKeyRec
{
    public XkbKeyNameRec Name;
    public short Gap;
    public byte ShapeNDX;
    public byte ColorNDX;
}