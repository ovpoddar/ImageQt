using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct XReply
{
    [FieldOffset(0)]
    public XGenericReply Generic;
    [FieldOffset(0)]
    public XGetGeometryReply Geom;
    [FieldOffset(0)] 
    public XQueryTreeReply Tree;
    [FieldOffset(0)]
    public XInternAtomReply Atom;
    [FieldOffset(0)]
    public XGetAtomNameReply AtomName;
    [FieldOffset(0)]
    public XGetPropertyReply Property;
    [FieldOffset(0)]
    public XListPropertiesReply ListProperties;
    [FieldOffset(0)]
    public XGetSelectionOwnerReply Selection;
    [FieldOffset(0)]
    public XGrabPointerReply GrabPointer;
    [FieldOffset(0)]
    public XGrabPointerReply GrabKeyboard;
    [FieldOffset(0)]
    public XQueryPointerReply Pointer;
    [FieldOffset(0)]
    public XGetMotionEventsReply MotionEvents;
    [FieldOffset(0)]
    public XTranslateCoordsReply Coords;
    [FieldOffset(0)]
    public XGetInputFocusReply InputFocus;
    [FieldOffset(0)]
    public XQueryTextExtentsReply TextExtents;
    [FieldOffset(0)]
    public XListFontsReply Fonts;
    [FieldOffset(0)]
    public XGetFontPathReply FontPath;
    [FieldOffset(0)]
    public XGetImageReply Image;
    [FieldOffset(0)]
    public XListInstalledColormapsReply Colormaps;
    [FieldOffset(0)]
    public XAllocColorReply AllocColor;
    [FieldOffset(0)]
    public XAllocNamedColorReply AllocNamedColor;
    [FieldOffset(0)]
    public XAllocColorCellsReply ColorCells;
    [FieldOffset(0)]
    public XAllocColorPlanesReply ColorPlanes;
    [FieldOffset(0)]
    public XQueryColorsReply Colors;
    [FieldOffset(0)]
    public XLookupColorReply LookupColor;
    [FieldOffset(0)]
    public XQueryBestSizeReply BestSize;
    [FieldOffset(0)]
    public XQueryExtensionReply Extension;
    [FieldOffset(0)]
    public XListExtensionsReply Extensions;
    [FieldOffset(0)]
    public XSetMappingReply SetModifierMapping;
    [FieldOffset(0)]
    public XGetModifierMappingReply GetModifierMapping;
    [FieldOffset(0)]
    public XSetMappingReply SetPointerMapping;
    [FieldOffset(0)]
    public XGetKeyboardMappingReply GetKeyboardMapping;
    [FieldOffset(0)]
    public XGetPointerMappingReply GetPointerMapping;
    [FieldOffset(0)]
    public XGetPointerControlReply PointerControl;
    [FieldOffset(0)]
    public XGetScreenSaverReply ScreenSaver;
    [FieldOffset(0)]
    public XListHostsReply Hosts;
    [FieldOffset(0)]
    public XError Error;
    [FieldOffset(0)]
    public XEvent Event;
}