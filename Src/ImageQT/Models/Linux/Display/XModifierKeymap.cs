using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XModifierKeymap
{
    public int MaxKeyPerMod; /* The server's max # of keys per modifier */
    public byte* ModifierMap; /* An 8 by maxKeyPerMod array of modifiers */
}