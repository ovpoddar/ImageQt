#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using ImageQT.Models.Mac;
using ImageQT.Models.Windows;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ImageQT.Handlers.Mac;
internal sealed class WindowManager : INativeWindowManager
{
    private bool _isRunning;
    private CGRect? _rect;
    private IntPtr? _nsView;
    private IntPtr? _window;

    public WindowManager()
    {
        // TODO: verify the changes. make a static class for nsApplication
        var app = NSApplication.SharedApplication;
        app.SetSetActivationPolicy(NSApplicationActivationPolicy.Regular);
        _isRunning = true;
    }

    public void CreateWindow(uint height, uint width)
    {
        _rect = new CGRect(0, 0, width, height);
        _nsView = ObjectCRuntime.PointerObjCMsgSend(
            ObjectCRuntime.PointerObjCMsgSend(ObjectCRuntime.ObjCGetClass("NSImageView"), PreSelector.Alloc),
            ObjectCRuntime.SelGetUid("initWithFrame:"),
            _rect.Value);
        _window = ObjectCRuntime.PointerObjCMsgSend(
            ObjectCRuntime.PointerObjCMsgSend(ObjectCRuntime.ObjCGetClass("NSWindow"), PreSelector.Alloc),
            ObjectCRuntime.SelGetUid("initWithContentRect:styleMask:backing:defer:"),
            _rect.Value,
            11,
            2,
            false);
    }

    public void Dispose()
    {
        if (_nsView.HasValue && _nsView != IntPtr.Zero)
            ObjectCRuntime.ObjCMsgSend(_nsView.Value, PreSelector.Release);
        if (_window.HasValue && _window != IntPtr.Zero)
            ObjectCRuntime.ObjCMsgSend(_window.Value, PreSelector.Release);
        GC.SuppressFinalize(this);
    }

    public Task Show(DateTime? closeTime = null)
    {
        if (!_window.HasValue || !_nsView.HasValue)
        {
            return Task.CompletedTask;
        }

        var app = NSApplication.SharedApplication;
        using (var delegateClass = new NSCustomClass(WindowWillClose))
        {

            ObjectCRuntime.ObjCMsgSend(_window.Value, ObjectCRuntime.SelGetUid("setDelegate:"), delegateClass);

            ObjectCRuntime.ObjCMsgSend(
               ObjectCRuntime.PointerObjCMsgSend(_window.Value, ObjectCRuntime.SelGetUid("contentView")),
               ObjectCRuntime.SelGetUid("addSubview:"),
               _nsView.Value);
            ObjectCRuntime.ObjCMsgSend(
              _window.Value,
              ObjectCRuntime.SelGetUid("makeKeyAndOrderFront:"),
              IntPtr.Zero);

            ObjectCRuntime.ObjCMsgSend(
              app,
              ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:"),
              false);

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
        if (!_rect.HasValue || !_nsView.HasValue)
        {
            return;
        }
        using var colorSpace = new NSString("NSCalibratedRGBColorSpace");
        var rep = ObjectCRuntime.PointerObjCMsgSend(
            ObjectCRuntime.PointerObjCMsgSend(ObjectCRuntime.ObjCGetClass("NSBitmapImageRep"), PreSelector.Alloc),
            ObjectCRuntime.SelGetUid("initWithBitmapDataPlanes:pixelsWide:pixelsHigh:bitsPerSample:samplesPerPixel:hasAlpha:isPlanar:colorSpaceName:bytesPerRow:bitsPerPixel:"),
            [image.Id],
            image.Width,
            image.Height,
            8,
            4,
            true,
            false,
            colorSpace,
            image.Width * Marshal.SizeOf<Pixels>(),
            32);
        var nsImage = ObjectCRuntime.PointerObjCMsgSend(
            ObjectCRuntime.PointerObjCMsgSend(ObjectCRuntime.ObjCGetClass("NSImage"), ObjectCRuntime.SelGetUid("alloc")),
            ObjectCRuntime.SelGetUid("initWithSize:"),
            _rect.Value.Size);

        ObjectCRuntime.ObjCMsgSend(
          nsImage,
          ObjectCRuntime.SelGetUid("addRepresentation:"),
          rep);
        ObjectCRuntime.ObjCMsgSend(
            _nsView.Value,
            ObjectCRuntime.SelGetUid("setImage:"),
            nsImage);
    }
}
#endif


