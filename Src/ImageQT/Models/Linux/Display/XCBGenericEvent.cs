using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XCBGenericEvent
{
    public byte ResponseType;
    public byte Pad0;
    public ushort Sequence;
    public fixed uint Pad[7];
    public uint FullSequence;
}