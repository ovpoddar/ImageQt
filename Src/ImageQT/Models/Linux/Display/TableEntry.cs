using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TableEntry
{
    public ulong RId;
    public int Context;
    public nint Data;
    public TableEntry* Next;
}