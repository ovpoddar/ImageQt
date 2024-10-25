#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using ImageQT.Models.Mac;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private readonly NSApplication _application;
    private CGRect? _cGRect;
    private NSImageView? _imageView;

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
        GC.SuppressFinalize(this);
    }

    public Task Show(DateTime? closeTime = null)
    {
        if (!_cGRect.HasValue || _imageView is null)
            return Task.CompletedTask;

        using var window = new NSWindow(_cGRect.Value);
        using var methodDelegate = new NSWindowDelegateImplementation(windowWillClose);
        var selector = ObjectCRuntime.SelGetUid("setDelegate:");
        ObjectCRuntime.ObjCMsgSend(window, selector, methodDelegate);
        selector = ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:");
        ObjectCRuntime.ObjCMsgSend(window, selector, IntPtr.Zero);

        using var time = new NSDate().DistantPast;
        using var mode = new NSString("kCFRunLoopDefaultMode");

        selector = ObjectCRuntime.SelGetUid("addSubview:");
        ObjectCRuntime.ObjCMsgSend(window.GetContentView(), selector, _imageView);

        while (_isRunning)
        {
            for (; ; )
            {
                if (closeTime != null && closeTime.Value < DateTime.Now)
                {
                    _isRunning = false;
                    break;
                }
                var @event = ObjectCRuntime.PointerObjCMsgSend(_application,
                    ObjectCRuntime.SelGetUid("nextEventMatchingMask:untilDate:inMode:dequeue:"),
                    NSEventMask.AnyEvent,
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
        if (_cGRect == null)
            return;

        _imageView = new NSImageView(_cGRect.Value);
        using var colorSpace = new NSString("NSCalibratedRGBColorSpace");
        using var imageRep = new NSBitmapImageRep(
            [image.Id],
            image.Width,
            image.Height,
            8, 4,
            true, false,
            colorSpace,
            image.Width * 4,
            32);
        using var nsImage = new NSImage(new(image.Width, image.Height));
        var selector = ObjectCRuntime.SelGetUid("addRepresentation:");
        ObjectCRuntime.ObjCMsgSend(nsImage, selector, imageRep);
        selector = ObjectCRuntime.SelGetUid("setImage:");
        ObjectCRuntime.ObjCMsgSend(_imageView, selector, nsImage);
    }
}
#endif
