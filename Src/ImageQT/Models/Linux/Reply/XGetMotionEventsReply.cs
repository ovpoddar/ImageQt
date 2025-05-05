using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGetMotionEventsReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length;
    public uint NEvents;
    public uint Pad2;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
}
