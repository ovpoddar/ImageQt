using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbRowRec
{
    public short Top;
    public short Left;
    public ushort NumKeys;
    public ushort SzKeys;
    public int Vertical;
    public XkbKeyRec* Keys;
    public XkbBoundsRec Bounds;
}