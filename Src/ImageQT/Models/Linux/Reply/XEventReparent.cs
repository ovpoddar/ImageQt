using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventReparent
{
    public uint Pad00;
    public uint Event;
    public uint Window;
    public uint Parent;
    public short X;
    public short Y;
    public byte Override;
    public byte Pad1;
    public byte Pad2;
    public byte Pad3;
}