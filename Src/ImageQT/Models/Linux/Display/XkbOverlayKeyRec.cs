using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbOverlayKeyRec
{
    public XkbKeyNameRec Over;
    public XkbKeyNameRec Under;
}