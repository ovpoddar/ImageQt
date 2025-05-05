using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XLookupColorReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public ushort ExactRed;
    public ushort ExactGreen;
    public ushort ExactBlue;
    public ushort ScreenRed;
    public ushort ScreenGreen;
    public ushort ScreenBlue;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
}