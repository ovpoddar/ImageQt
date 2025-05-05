using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XCrossingEvent
{
    public EventType Type;       /* of event */
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Window;          /* "event" window reported relative to */
    public ulong Root;            /* root window that the event occurred on */
    public ulong SubWindow;   /* child window */
    public ulong Time;      /* milliseconds */
    public int X;
    public int Y; /* pointer x, y coordinates in event window */
    public int XRoot;
    public int YRoot; /* coordinates relative to root */
    public int Mode;       /* NotifyNormal, NotifyGrab, NotifyUngrab */
    public int Detail;
    /*
	 * NotifyAncestor, NotifyVirtual, NotifyInferior,
	 * NotifyNonlinear,NotifyNonlinearVirtual
	 */
    public int SameScreen;   /* same screen flag */
    public int Focus;     /* intean focus */
    public uint State;	/* key or button mask */
}



