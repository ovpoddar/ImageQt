#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            NSWindowStyle.Borderless | NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable,
            NSBackingStore.Buffered,
            false));
    }

    public IntPtr ContentView =>
        ObjectCRuntime.PointerObjCMsgSend(this, ObjectCRuntime.SelGetUid("contentView"));

    public void MakeKeyAndOrderFront(IntPtr sender)
    {
        var selector = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(this, selector, sender);
    }

    public void SetDelegate(NSCustomClass delegateClass)
    {
        var selector = ObjectCRuntime.SelGetUid("setDelegate:");
        ObjectCRuntime.ObjCMsgSend(this, selector, delegateClass);
    }
}
#endif