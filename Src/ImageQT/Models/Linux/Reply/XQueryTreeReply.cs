using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XQueryTreeReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length;
    public uint Root;
    public uint Parent;
    public ushort NChildren;
    public ushort Pad2;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
}
