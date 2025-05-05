using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public struct XkbSymInterpretRec
{
    public ulong Sym;
    public byte Flags;
    public byte Match;
    public byte Mods;
    public byte VirtualMod;
    public XkbAnyAction Act;
}