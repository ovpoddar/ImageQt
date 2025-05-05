using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGrabPointerReply
{
    public byte Type; /* X_Reply */
    public byte Status;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public uint Pad1;
    public uint Pad2;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
}
