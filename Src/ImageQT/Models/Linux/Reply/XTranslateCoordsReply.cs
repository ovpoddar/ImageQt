using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XTranslateCoordsReply
{
    public byte Type; /* X_Reply */
    public byte SameScreen;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public uint Child;
    public short DstX;
    public short DstY;
    public uint Pad2;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
}
