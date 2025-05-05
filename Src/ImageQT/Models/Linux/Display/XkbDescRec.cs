using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbDescRec
{
    public XDisplay* Dpy;
    public ushort Flags;
    public ushort DeviceSpec;
    public byte MinKeyCode;
    public byte MaxKeyCode;

    public XkbControlsRec* Ctrls;
    public XkbServerMapRec* Server;
    public XkbClientMapRec* Map;
    public nint Indicators; // todo: need to a way to implement XkbIndicatorRec
    public XkbNamesRec* Names;
    public nint Compat; // todo: need to a way to implement XkbCompatMapRec
    public _XkbGeometry* Geom;
}