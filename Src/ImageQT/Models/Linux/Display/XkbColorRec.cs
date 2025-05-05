using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbColorRec
{
    public uint Pixel;
    public nint Spec;
}