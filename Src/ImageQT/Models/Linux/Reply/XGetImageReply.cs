using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGetImageReply
{
    public byte Type; /* X_Reply */
    public byte Depth;
    public ushort SequenceNumber;
    public uint Length;
    public uint Visual;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
    public uint Pad7;
}
