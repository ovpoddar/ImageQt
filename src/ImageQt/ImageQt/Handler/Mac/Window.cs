
using ImageQt.CallerPInvoke.Mac;
using ImageQt.Models.Mac;

namespace ImageQt.Handler.Mac;

internal class Window : IWindow
{
    private IntPtr _app;
    public void CleanUpResources(ref nint window)
    {
        var terminate = ObjectCRuntime.SelGetUid("terminate:");
        ObjectCRuntime.ObjCMsgSend(_app, terminate);

        if (window != IntPtr.Zero)
        {
            var release = ObjectCRuntime.SelGetUid("release");
            ObjectCRuntime.Void_ObjCMsgSend(window, release);
            window = IntPtr.Zero;
        }

        if (_app != IntPtr.Zero)
        {
            var release = ObjectCRuntime.SelGetUid("release");
            ObjectCRuntime.Void_ObjCMsgSend(_app, release);
            _app = IntPtr.Zero;
        }
    }

    public nint DeclareWindow(string windowTitle, uint height, uint width)
    {
        InitilizedApplication();

        var setActivationPolicy = ObjectCRuntime.SelGetUid("setActivationPolicy:");
        ObjectCRuntime.ObjCMsgSend(_app, setActivationPolicy, 0);

        var nsWindow = ObjectCRuntime.ObjCGetClass("NSWindow");
        var alloc = ObjectCRuntime.SelGetUid("alloc");
        var nsWindowObject = ObjectCRuntime.ObjCMsgSend(nsWindow, alloc);
        var defer = ObjectCRuntime.SelGetUid("initWithContentRect:styleMask:backing:defer:");
        CGRect cGRect = new(0, 0, width, height);
        var window = ObjectCRuntime.ObjCMsgSend(nsWindowObject, defer, cGRect, NSWindowStyleMask.Titled | NSWindowStyleMask.Closable | NSWindowStyleMask.Resizable, NSBackingStore.Buffered, false);

        var utfString = ObjectCRuntime.SelGetUid("stringWithUTF8String:");
        var strClass = ObjectCRuntime.ObjCGetClass("NSString");
        var title = ObjectCRuntime.ObjCMsgSend(strClass, utfString, windowTitle);

        var setTitle = ObjectCRuntime.SelGetUid("setTitle:");
        ObjectCRuntime.ObjCMsgSend(window, setTitle, title);

        return window;
    }

    public void LoadBitMap(int width, int height, ref nint ImageData, IntPtr display)
    {
        throw new NotImplementedException();
    }

    public void ProcessEvent(nint window)
    {
        var run = ObjectCRuntime.SelGetUid("run");
        ObjectCRuntime.ObjCMsgSend(_app, run);
    }

    public void ShowWindow(nint window)
    {
        var showIns = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(window, showIns, IntPtr.Zero);

        var makeitTop = ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:");
        ObjectCRuntime.ObjCMsgSend(_app, makeitTop, true);
    }

    private void InitilizedApplication()
    {
        if (_app != IntPtr.Zero)
            return;

        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        var sharedApplication = ObjectCRuntime.SelGetUid("sharedApplication");
        _app = ObjectCRuntime.ObjCMsgSend(nsApplication, sharedApplication);

    }
}
