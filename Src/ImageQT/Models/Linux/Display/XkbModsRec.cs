using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbModsRec
{
    public byte Mask; /* effective mods */
    public byte RealMods;
    public ushort VMods;
}