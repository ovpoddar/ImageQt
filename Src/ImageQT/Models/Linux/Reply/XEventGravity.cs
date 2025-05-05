using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventGravity
{
    public uint Pad00;
    public uint Event;
    public uint Window;
    public short X;
    public short Y;
    public uint Pad1;
    public uint Pad2;
    public uint Pad3;
    public uint Pad4;
}