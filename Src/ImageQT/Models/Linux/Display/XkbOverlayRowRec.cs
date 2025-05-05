using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbOverlayRowRec
{
    public ushort RowUnder;
    public ushort NumKeys;
    public ushort SzKeys;
    public XkbOverlayKeyRec* Keys;
}