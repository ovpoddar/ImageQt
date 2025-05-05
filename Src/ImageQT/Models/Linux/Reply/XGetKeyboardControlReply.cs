using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XGetKeyboardControlReply
{
    public byte Type; /* X_Reply */
    public byte GlobalAutoRepeat;
    public ushort SequenceNumber;
    public uint Length; /* 5 */
    public uint LedMask;
    public byte KeyClickPercent;
    public byte BellPercent;
    public ushort BellPitch;
    public ushort BellDuration;
    public ushort Pad;
    public fixed byte map[32]; /* bit masks start here */
}