using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Linux;
[StructLayout(LayoutKind.Sequential, Size = (24 * sizeof(long)))]

public struct XAnyEvent
{
    public Event type;
    public ulong serial;
    public bool send_event;
    public IntPtr display;
    public ulong window;

}
