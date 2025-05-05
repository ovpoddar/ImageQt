using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XRMMethods
{
    public delegate* unmanaged[Cdecl]<nint, void> MbInit;
    public delegate* unmanaged[Cdecl]<nint, sbyte*, int*, sbyte> MbChar;
    public delegate* unmanaged[Cdecl]<nint, void> MbFinish;
    public delegate* unmanaged[Cdecl]<nint, nint> LcName;
    public delegate* unmanaged[Cdecl]<nint, void> Destroy;
}