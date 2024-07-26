#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal struct NSColor
{
    public IntPtr _stackPtr;
    public NSColor(NSImage patternImage)
    {
        var colorClass = ObjectCRuntime.ObjCGetClass("NSColor");
        var selector = ObjectCRuntime.SelGetUid("colorWithPatternImage:");
        _stackPtr = ObjectCRuntime.PointerObjCMsgSend(colorClass, selector, patternImage);
    }

    public static implicit operator IntPtr(NSColor color) =>
        color._stackPtr;
}
#endif
