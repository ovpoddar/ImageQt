using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbConverters
{
    public delegate* unmanaged[Cdecl]<ulong, ulong, char*, int, int*, int> KSToMB;
    public nint KSToMBPriv;
    public delegate* unmanaged[Cdecl]<ulong, char*, int, int*, ulong> MBToKS;
    public nint MBToKSPriv;
    public delegate* unmanaged[Cdecl]<ulong, ulong> KSToUpper;
}