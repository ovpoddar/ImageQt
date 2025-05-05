using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbKTMapEntryRec
{
    public int Active;
    public byte Level;
    public XkbModsRec Mods;
}