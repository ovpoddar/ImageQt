#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSWindow : SafeHandleBaseZeroInvalid
{
    public NSWindow(CGRect cgRect) : base(true)
    {
        var nsWindow = Appkit.ObjCGetClass("NSWindow");
        var window = ObjectCRuntime.PointerObjCMsgSend(nsWindow, PreSelector.Alloc);

        var selector = ObjectCRuntime.SelGetUid("initWithContentRect:styleMask:backing:defer:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(window,
            selector,
            cgRect,
            NSWindowStyle.Borderless |
            NSWindowStyle.Titled |
            NSWindowStyle.Closable |
            NSWindowStyle.Miniaturizable |
            NSWindowStyle.Resizable,
            NSBackingStore.Buffered,
            false));
    }

}
#endif
