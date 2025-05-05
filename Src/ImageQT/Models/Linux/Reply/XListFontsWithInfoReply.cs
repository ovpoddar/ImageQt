using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Reply;

/* Warning: this MUST match (up to component renaming) xQueryFontReply */
[StructLayout(LayoutKind.Sequential)]
public struct XListFontsWithInfoReply
{
    public byte Type;       /* X_Reply */
    public byte NameLength; /* 0 indicates end-of-reply-sequence */
    public ushort SequenceNumber;
    public uint Length; /* definitely > 0, even if "nameLength" is 0 */
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
    public uint NReplies; /* hint as to how many more replies might be coming */
}
