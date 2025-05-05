using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Reply;

/* Warning: this MUST match (up to component renaming) xListFontsWithInfoReply */
[StructLayout(LayoutKind.Sequential)]
public struct XQueryFontReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* definitely > 0, even if "nCharInfos" is 0 */
    XCharInfo MinBounds;
    public uint Walign1;
    XCharInfo MaxBounds;
    public uint Walign2;
    public ushort MinCharOrByte2;
    public ushort MaxCharOrByte2;
    public ushort DefaultChar;
    public ushort NFontProps; /* followed by this many xFontProp structures */
    public byte DrawDirection;
    public byte MinByte1;
    public byte MaxByte1;
    public byte AllCharsExist;
    public short FontAscent;
    public short FontDescent;
    public uint NCharInfos; /* followed by this many xCharInfo structures */
}
