using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct FreeFuncs
{
    public delegate* unmanaged[Cdecl]<XDisplay*, void> Atoms;
    public delegate* unmanaged[Cdecl]<XModifierKeymap*, void> ModifierMap;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> KeyBindings;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> ContextDB;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> DefaultCCCs;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> ClientCmaps;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> IntensityMaps;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> IMFilters;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> XKB;
}