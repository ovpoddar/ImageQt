using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XkbOverlay
{
    public ulong Name;
    public XkbSectionRec* SectionUnder;
    public ushort NumRows;
    public ushort SzRows;
    public XkbOverlayRowRec* Rows;
    public XkbBoundsRec* Bounds;
}