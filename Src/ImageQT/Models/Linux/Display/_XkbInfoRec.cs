using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XkbInfoRec
{
    public uint Flags;
    public uint XlibCtrls;
    public XExtCodes* Codes;
    public int SrvMajor;
    public int SrvMinor;
    public uint SelectedEvents;
    public ushort SelectedNknDetails;
    public ushort SelectedMapDetails;
    public XkbDescRec* Desc;
    public XkbMapChangesRec Changes;
    public ulong ComposeLED;
    public XkbConverters Cvt;
    public XkbConverters Latin1cvt;
}