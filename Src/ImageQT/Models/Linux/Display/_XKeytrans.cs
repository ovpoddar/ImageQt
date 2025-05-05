using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XKeytrans
{
    public _XKeytrans* Next; /* next on list */
    public nint String; /* string to return when the time comes */
    public int Length; /* length of string (since NULL is legit) */
    public ulong Key; /* keysym rebound */
    public uint State; /* modifier state */
    public ulong* Modifiers; /* modifier keysyms you want */
    public int ModifierLength; /* length of modifier list */
}