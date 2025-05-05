using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbControlsRec
{
    public byte MkDfltBtn;
    public byte NumGroups;
    public byte GroupsWrap;
    public XkbModsRec Internal;
    public XkbModsRec IgnoreLock;
    public uint EnabledCtrls;
    public ushort RepeatDelay;
    public ushort RepeatInterval;
    public ushort SlowKeysDelay;
    public ushort DebounceDelay;
    public ushort MkDelay;
    public ushort MkInterval;
    public ushort MkTimeToMax;
    public ushort MkMaxSpeed;
    public short MkCurve;
    public ushort AxOptions;
    public ushort AxTimeout;
    public ushort AxtOptsMask;
    public ushort AxtOptsValues;
    public uint AxtCtrlsMask;
    public uint AxtCtrlsValues;
    public fixed byte PerKeyRepeat[32];
}