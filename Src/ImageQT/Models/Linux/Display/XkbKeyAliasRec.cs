using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbKeyAliasRec
{
    public fixed sbyte Real[4];
    public fixed sbyte Alias[4];
}