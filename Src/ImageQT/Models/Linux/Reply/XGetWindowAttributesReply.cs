using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

/* Individual reply formats. */
[StructLayout(LayoutKind.Sequential)]
public struct XGetWindowAttributesReply
{
    public byte Type; /* X_Reply */
    public byte BackingStore;
    public ushort SequenceNumber;
    public uint Length; /* NOT 0; this is an extra-large reply */
    public uint VisualID;
    public ushort Class;
    public byte BitGravity;
    public byte WinGravity;
    public uint BackingBitPlanes;
    public uint BackingPixel;
    public byte SaveUnder;
    public byte MapInstalled;
    public byte MapState;
    public byte Override;
    public uint Colormap;
    public uint AllEventMasks;
    public uint YourEventMask;
    public ushort DoNotPropagateMask;
    public ushort Pad;
}
