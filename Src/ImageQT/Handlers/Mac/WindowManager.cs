using ImageQT.DllInterop.Mac;
using ImageQT.Models.Mac;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private NSApplication? _application;
    private CGRect _cGRect;
    public nint CreateWindow(uint height, uint width)
    {
        _isRunning = true;

        _cGRect = new(0, 0, width, height);
        _application = new NSApplication();
        var setActivationPolicy = ObjectCRuntime.SelGetUid("setActivationPolicy:");
        ObjectCRuntime.BoolObjCMsgSend(_application, setActivationPolicy, 0);
        var makeitTop = ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:");
        ObjectCRuntime.ObjCMsgSend(_application, makeitTop, true);

        return default;
    }

    public void Dispose()
    {
        if (_application == null) 
            return;

        if (!_application.IsInvalid)
        {
            _application.Dispose();
        }
    }

    public Task Show()
    {
        using var window = new NSWindow(_cGRect);
        using var methodDelegate = new NSWindowDelegateImplementation(windowWillClose);
        var selector = ObjectCRuntime.SelGetUid("setDelegate:");
        ObjectCRuntime.ObjCMsgSend(window, selector, methodDelegate);
        selector = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(window, selector, IntPtr.Zero);

        using var time = new NSDate().DistantPast;
        using var mode = new NSString("kCFRunLoopDefaultMode");
        while (_isRunning)
        {
            for (; ; )
            {
                var @event = ObjectCRuntime.PointerObjCMsgSend(_application,
                    ObjectCRuntime.SelGetUid("nextEventMatchingMask:untilDate:inMode:dequeue:"),
                    18446744073709551615U,
                    time,
                    mode,
                    true);

                if (@event == IntPtr.Zero)
                    break;

                ObjectCRuntime.PointerObjCMsgSend(_application, ObjectCRuntime.SelGetUid("sendEvent:"), @event);
            }
        }
        return Task.CompletedTask;
    }

    void windowWillClose(IntPtr receiver, IntPtr selector, IntPtr arguments)
    {
        if (_isRunning)
        {
            _isRunning = false;
        }
    }
}
