using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGetPropertyReply
{
    public byte Type; /* X_Reply */
    public byte Format;
    public ushort SequenceNumber;
    public uint Length; /* of additional bytes */
    public uint PropertyType;
    public uint BytesAfter;
    public uint NItems; /* # of 8, 16, or 32-bit entities in reply */
    public uint Pad1;
    public uint Pad2;
    public uint Pad3;
}
