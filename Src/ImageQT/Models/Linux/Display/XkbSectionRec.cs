using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbSectionRec
{
    public ulong name;
    public byte priority;
    public short top;
    public short left;
    public ushort width;
    public ushort height;
    public short angle;
    public ushort num_rows;
    public ushort num_doodads;
    public ushort num_overlays;
    public ushort sz_rows;
    public ushort sz_doodads;
    public ushort sz_overlays;
    public XkbRowRec* rows;
    public nint doodads; // todo: implement this XkbDoodadRec
    public XkbBoundsRec bounds;
    public _XkbOverlay* overlays;
}