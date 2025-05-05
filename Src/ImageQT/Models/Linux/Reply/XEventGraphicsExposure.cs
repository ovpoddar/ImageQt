using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventGraphicsExposure
{
    public uint Pad00;
    public uint Drawable;
    public ushort X;
    public ushort Y;
    public ushort Width;
    public ushort Height;
    public ushort MinorEvent;
    public ushort Count;
    public byte MajorEvent;
    public byte Pad1;
    public byte Pad2;
    public byte Pad3;
}