using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbPropertyRec
{
    public nint Name;
    public nint Value;
}