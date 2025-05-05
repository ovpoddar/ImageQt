using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XEventKeyButtonPointer
{
    public uint Pad00;
    public uint Time;
    public uint Root;
    public uint Event;
    public uint Child;
    public short RootX;
    public short RootY;
    public short EventX;
    public short EventY;
    public ushort State;
    public byte SameScreen;
    public byte Pad1;
}
