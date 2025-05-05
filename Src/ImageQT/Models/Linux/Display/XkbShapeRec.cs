using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbShapeRec
{
    public ulong Name;
    public ushort NumOutlines;
    public ushort SzOutlines;
    public XkbOutlineRec* Outlines;
    public XkbOutlineRec* Approx;
    public XkbOutlineRec* Primary;
    public XkbBoundsRec Bounds;
}