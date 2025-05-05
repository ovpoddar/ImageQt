using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Explicit)]
public struct XEvent
{
    [FieldOffset(0)]
    public XEventU U;
    [FieldOffset(0)]
    public XEventKeyButtonPointer KeyButtonPointer;
    [FieldOffset(0)]
    public XEventEnterLeave EnterLeave;
    [FieldOffset(0)]
    public XEventFocus Focus;
    [FieldOffset(0)]
    public XEventExpose Expose;
    [FieldOffset(0)]
    public XEventGraphicsExposure GraphicsExposure;
    [FieldOffset(0)]
    public XEventNoExposure NoExposure;
    [FieldOffset(0)]
    public XEventVisibility Visibility;
    [FieldOffset(0)]
    public XEventCreateNotify CreateNotify;
    //* The event fields in the structures for DestroyNotify, UnmapNotify,
    //* MapNotify, ReparentNotify, ConfigureNotify, CirculateNotify, GravityNotify,
    //* must be at the same offset because server internal code is depending upon
    //* this to patch up the events before they are delivered.
    //* Also note that MapRequest, ConfigureRequest and CirculateRequest have
    //* the same offset for the event window.
    [FieldOffset(0)]
    public XEventDestroyNotify DestroyNotify;
    [FieldOffset(0)]
    public XEventUnmapNotify UnMapNotify;
    [FieldOffset(0)]
    public XEventMapNotify MapNotify;
    [FieldOffset(0)]
    public XEventMapRequest MapRequest;
    [FieldOffset(0)]
    public XEventReparent Reparent;
    [FieldOffset(0)]
    public XEventConfigureNotify ConfigureNotify;
    [FieldOffset(0)]
    public XEventConfigureRequest ConfigureRequest;
    [FieldOffset(0)]
    public XEventGravity Gravity;
    [FieldOffset(0)]
    public XEventResizeRequest ResizeRequest;
    [FieldOffset(0)]
    public XEventCirculate Circulate;
    [FieldOffset(0)]
    public XEventProperty Property;
    [FieldOffset(0)]
    public XEventSelectionClear SelectionClear;
    [FieldOffset(0)]
    public XEventSelectionRequest SelectionRequest;
    [FieldOffset(0)]
    public XEventSelectionNotify SelectionNotify;
    [FieldOffset(0)]
    public XEventColorMap ColorMap;
    [FieldOffset(0)]
    public XEventMappingNotify MappingNotify;
    [FieldOffset(0)]
    public XEventClientMessage ClientMessage;
 }