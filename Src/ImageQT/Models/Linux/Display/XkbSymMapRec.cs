using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbSymMapRec
{
    public fixed byte KTIndex[4];
    public byte GroupInfo;
    public byte Width;
    public ushort Offset;
}