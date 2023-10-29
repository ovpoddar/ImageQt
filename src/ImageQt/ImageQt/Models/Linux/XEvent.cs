using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Linux;
[StructLayout(LayoutKind.Sequential, Size = (24 * sizeof(long)))]
public struct XEvent
{
    public int type;
    public XAnyEvent xany;
    public XKeyEvent xkey;
    public XButtonEvent xbutton;
    public XMotionEvent xmotion;
    public XCrossingEvent xcrossing; 
    public XFocusChangeEvent xfocus;
    public XExposeEvent xexpose;
    public XGraphicsExposeEvent xgraphicsexpose;
    public XNoExposeEvent xnoexpose; 
    public XVisibilityEvent xvisibility;
    public XCreateWindowEvent xcreatewindow; //72
    public XDestroyWindowEvent xdestroywindow; //48
    public XUnmapEvent xunmap; //56
    public XMapEvent xmap; //56
    public XMapRequestEvent xmaprequest; //48
    public XReparentEvent xreparent; //72
    public XConfigureEvent xconfigure; //88
    public XGravityEvent xgravity; //56
    public XResizeRequestEvent xresizerequest; //48
    public XConfigureRequestEvent xconfigurerequest; //96
    public XCirculateEvent xcirculate;// 56
    public XCirculateRequestEvent xcirculaterequest; //56
    public XPropertyEvent xproperty; //64
    public XSelectionClearEvent xselectionclear; //56
    public XSelectionRequestEvent xselectionrequest; //80
    public XSelectionEvent xselection; // 72
    public XColormapEvent xcolormap; //56
    public XClientMessageEvent xclient; //96
    public XMappingEvent xmapping; //56
    public XErrorEvent xerror; //40
    public XKeymapEvent xkeymap; //72
    public XGenericEvent xgeneric; //40
    public XGenericEventCookie xcookie; //56
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)] //24
    public long[] pad;
    // You may also need to define constants for event types, e.g., KeyPress, ButtonPress, MotionNotify, etc.
}