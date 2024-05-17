using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using ImageQT.Models.Mac;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private readonly NSApplication _application;
    private CGRect? _cGRect;
    private NSImage? _image;

    public WindowManager()
    {
        _isRunning = true;
        _application = new NSApplication();
    }

    public nint CreateWindow(uint height, uint width)
    {
        _cGRect = new(0, 0, width, height);
        var setActivationPolicy = ObjectCRuntime.SelGetUid("setActivationPolicy:");
        ObjectCRuntime.BoolObjCMsgSend(_application, setActivationPolicy, 0);
        var makeitTop = ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:");
        ObjectCRuntime.ObjCMsgSend(_application, makeitTop, true);

        return default;
    }

    public void Dispose()
    {

        if (!_application.IsInvalid)
        {
            _application.Dispose();
        }
    }

    public Task Show()
    {
        if (!_cGRect.HasValue || _image is null)
            return Task.CompletedTask;

        using var window = new NSWindow(_cGRect.Value);
        using var methodDelegate = new NSWindowDelegateImplementation(windowWillClose);
        var selector = ObjectCRuntime.SelGetUid("setDelegate:");
        ObjectCRuntime.ObjCMsgSend(window, selector, methodDelegate);
        selector = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(window, selector, IntPtr.Zero);

        using var time = new NSDate().DistantPast;
        using var mode = new NSString("kCFRunLoopDefaultMode");

        var color = new NSColor(_image);
        selector = ObjectCRuntime.SelGetUid("setBackgroundColor:");
        ObjectCRuntime.ObjCMsgSend(window, selector, color);

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

    public void SetUpImage(Image image)
    {
        using var colorSpace = new NSString("NSCalibratedRGBColorSpace");
        using var imageRep = new NSBitmapImageRep(
            [image.Id],
            image.Width,
            image.Height,
            8,4,
            true, false,
            colorSpace,
            image.Width * 4,
            32);
        _image = new NSImage(new(image.Width, image.Height));

        var selector = ObjectCRuntime.SelGetUid("addRepresentation:");
        ObjectCRuntime.ObjCMsgSend(_image, selector, imageRep);
    }
}
