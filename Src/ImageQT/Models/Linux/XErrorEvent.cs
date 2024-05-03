using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XErrorEvent
{

    public int type;
    public IntPtr display;   /* Display the event was read from */
    public ulong resourceid;     /* resource id */
    public ulong serial;   /* serial number of failed request */
    public byte error_code;   /* error code of failed request */
    public byte request_code; /* Major op-code of failed request */
    public byte minor_code;
}