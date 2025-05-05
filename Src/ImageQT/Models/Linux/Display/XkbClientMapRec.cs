using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbClientMapRec
{
    /* types is an array of XkbKeyTypeRec structs, with size_types entries
       allocated, and num_types entries used. */
    public byte SizeTypes;
    public byte NumTypes;
    public XkbKeyTypeRec* Types;

    /* syms is an array of sizeSyms KeySyms, in which numSyms are used */
    public ushort SizeSyms;
    public ushort NumSyms;

    public ulong* Syms;

    /* keySymMap is an array of (maxKeyCode + 1) XkbSymMapRec structs */
    public XkbSymMapRec* KeySymMap;

    /* modmap is an array of (maxKeyCode + 1) unsigned chars */
    public nint Modmap;
}