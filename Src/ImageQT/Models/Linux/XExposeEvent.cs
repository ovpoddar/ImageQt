﻿#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XExposeEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong window;
    public int x, y;
    public int width, height;
    public int count;		/* if non-zero, at least this many more */
}
#endif
