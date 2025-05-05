using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbNamesRec
{
    public ulong Keycodes;
    public ulong Geometry;
    public ulong Symbols;
    public ulong Types;
    public ulong Compat;
    public fixed ulong Vmods[16];
    public fixed ulong Indicators[32];

    public fixed ulong Groups[4];

    /* keys is an array of (xkb->max_key_code + 1) XkbKeyNameRec entries */
    public XkbKeyNameRec* Keys;

    /* key_aliases is an array of num_key_aliases XkbKeyAliasRec entries */
    public XkbKeyAliasRec* KeyAliases;

    /* radioGroups is an array of numRg Atoms */
    public ulong* RadioGroups;
    public ulong PhysSymbols;

    /* numKeys seems to be unused in libX11 */
    public byte NumKeys;
    public byte NumKeyAliases;
    public ushort NumRg;
}