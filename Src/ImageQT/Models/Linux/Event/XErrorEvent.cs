using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XErrorEvent
{
    public EventType Type;
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong ResourceId;     /* resource id */
    public ulong Serial;   /* serial number of failed request */
    public byte ErrorCode;   /* error code of failed request */
    public byte RequestCode; /* Major op-code of failed request */
    public byte MinorCode;   /* Minor op-code of failed request */
}
