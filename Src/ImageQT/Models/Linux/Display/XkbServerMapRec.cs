using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbServerMapRec
{
    public ushort NumActs;
    public ushort SizeActs;
    public nint Acts; // todo: need to implement this XkbAction

    /* behaviors, keyActs, explicit, & vmodmap are all arrays with
       (xkb->maxKeyCode + 1) entries allocated for each. */
    public XkbBehavior* Behaviors;
    public ushort* KeyActs;
    public byte* Explicit;
    public fixed byte VMods[16];
    public ushort* VModMap;
}