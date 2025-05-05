using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XQueryExtensionReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public byte Present;
    public byte Major_opcode;
    public byte First_event;
    public byte First_error;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
    public uint Pad7;
}