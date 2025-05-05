using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbIndicatorMapRec
{
    public byte Flags;
    public byte WhichGroups;
    public byte Groups;
    public byte WhichMods;
    public XkbModsRec Mods;
    public uint Ctrls;
}