#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using ImageQT.Models.Mac;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private NSImageView? _nsView;
    private NSWindow? _window;

    public WindowManager()
    {
        _isRunning = true;
        var app = NSApplication.SharedApplication;
        app.SetSetActivationPolicy(NSApplicationActivationPolicy.Regular);
    }

    public void CreateWindow(uint height, uint width)
    {
        var rect = new CGRect(0, 0, width, height);
        _nsView = new NSImageView(rect);
        _window = new NSWindow(rect);

        _window.ContentView.AddSubview(_nsView);
        _window.MakeKeyAndOrderFront(IntPtr.Zero);
    }

    public void Dispose()
    {
        if (_nsView != null && _nsView.IsClosed)
            _nsView.Dispose();

        if (_window != null && !_window.IsClosed)
            _window.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task Show(DateTime? closeTime = null)
    {
        if (_window == null || _window.IsClosed || _window.IsInvalid)
        {
            return Task.CompletedTask;
        }

        using var delegateClass = new NSCustomClass(WindowWillClose);
        _window.SetDelegate(delegateClass);

        var app = NSApplication.SharedApplication;
        app.ActivateIgnoringOtherApps(true);

        using var mode = new NSString("kCFRunLoopDefaultMode");
        using var time = new NSDate();
        while (this._isRunning)
        {
            for (; ; )
            {
                var evnt = ObjectCRuntime.PointerObjCMsgSend(
                    app,
                    ObjectCRuntime.SelGetUid("nextEventMatchingMask:untilDate:inMode:dequeue:"),
                    ulong.MaxValue,
                    time,
                    mode,
                    true);
                if (evnt == IntPtr.Zero) break;

                ObjectCRuntime.ObjCMsgSend(app, ObjectCRuntime.SelGetUid("sendEvent:"), evnt);
                ObjectCRuntime.ObjCMsgSend(evnt, PreSelector.Release);
            }

        }
        return Task.CompletedTask;

    }

    void WindowWillClose(IntPtr receiver, IntPtr selector, IntPtr arguments)
    {
        if (_isRunning)
        {
            _isRunning = false;
        }
    }

    public void SetUpImage(Image image)
    {
        if (_nsView == null || _nsView.IsClosed || _nsView.IsInvalid)
        {
            return;
        }
        using var colorSpace = new NSString("NSCalibratedRGBColorSpace");
        var rep = new NSBitmapImageRep([image.Id], image.Width, image.Height, 8, 4,
            true, false, colorSpace, image.Width * 4, 32);
        var nsImage = new NSImage(new CGSize(image.Width, image.Height));
        nsImage.AddRepresentation(rep);

        _nsView.SetImage(nsImage);
    }
}
#endif


