using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XGetModifierMappingReply
{
    public byte Type;
    public byte NumKeyPerModifier;
    public ushort SequenceNumber;
    public uint Length;
    public uint Pad1;
    public uint Pad2;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
}