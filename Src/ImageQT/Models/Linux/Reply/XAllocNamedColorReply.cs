using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XAllocNamedColorReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public uint Pixel;
    public ushort ExactRed;
    public ushort ExactGreen;
    public ushort ExactBlue;
    public ushort ScreenRed;
    public ushort ScreenGreen;
    public ushort ScreenBlue;
    public uint Pad2;
    public uint Pad3;
}
