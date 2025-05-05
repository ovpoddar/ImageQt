using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbOutlineRec
{
    public ushort NumPoints;
    public ushort SzPoints;
    public ushort CornerRadius;
    public XkbPointRec* Points;
}