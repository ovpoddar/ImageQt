using ImageQT.DllInterop.Mac;
using ImageQT.Models.Mac;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private NSWindow _window;
    private NSWindowDelegateImplementation _delegate;
    public nint CreateWindow(uint height, uint width)
    {
        _isRunning = true;

        CGRect cGRect = new(0, 0, width, height);
        SingletonNSApplication.NSInitialized();
        _window = new NSWindow();
        var selector = ObjectCRuntime.SelGetUid("initWithContentRect:styleMask:backing:defer:");
        _window = ObjectCRuntime.NSWindowObjCMsgSend(_window, selector, cGRect, 15, 2, false);
        _delegate = new NSWindowDelegateImplementation(windowWillClose);
        selector = ObjectCRuntime.SelGetUid("setDelegate:");
        ObjectCRuntime.ObjCMsgSend(_window, selector, _delegate);
        using var title = new NSString("mac final step");
        selector = ObjectCRuntime.SelGetUid("setTitle:");
        ObjectCRuntime.ObjCMsgSend(_window, selector, title);
        selector = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(_window, selector, IntPtr.Zero);
        return default;
    }

    public void Dispose()
    {
        if (!_window.IsInvalid)
        {
            _window.Dispose();
        }
        if (!_delegate.IsInvalid)
        {
            _delegate.Dispose();
        }
    }

    public Task Show()
    {

        using var time = new NSDate().DistantPast;
        using var mode = new NSString("kCFRunLoopDefaultMode");
        while (_isRunning)
        {
            for (; ; )
            {
                var @event = ObjectCRuntime.PointerObjCMsgSend(SingletonNSApplication.NSApplication,
                    ObjectCRuntime.SelGetUid("nextEventMatchingMask:untilDate:inMode:dequeue:"),
                    18446744073709551615U,
                    time,
                    mode,
                    true);

                if (@event == IntPtr.Zero)
                    break;

                ObjectCRuntime.PointerObjCMsgSend(SingletonNSApplication.NSApplication, ObjectCRuntime.SelGetUid("sendEvent:"), @event);
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
