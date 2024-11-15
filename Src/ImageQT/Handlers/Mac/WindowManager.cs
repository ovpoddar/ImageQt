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
        _rect = new CGRect(0, 0, width, height);
        _nsView = new NSImageView(_rect.Value);
        _window = new NSWindow(_rect.Value);

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
        if (!_rect.HasValue || _nsView == null || _nsView.IsClosed || _nsView.IsInvalid)
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
            ObjectCRuntime.PointerObjCMsgSend(ObjectCRuntime.ObjCGetClass("NSImage"), PreSelector.Alloc),
            ObjectCRuntime.SelGetUid("initWithSize:"),
            _rect.Value.Size);

        ObjectCRuntime.ObjCMsgSend(
          nsImage,
          ObjectCRuntime.SelGetUid("addRepresentation:"),
          rep);

        _nsView.SetImage(nsImage);
    }
}

internal class NSImage
{
    private readonly IntPtr _handle;
    public NSImage(CGSize size)
    {
        var nsImage = Appkit.ObjCGetClass("NSImage");
        var image = ObjectCRuntime.PointerObjCMsgSend(nsImage, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithSize:");
        _handle = ObjectCRuntime.PointerObjCMsgSend(image, selector, size);
    }

    public void AddRepresentation(NSBitmapImageRep bitmapImageRep)
    {
        var selector = ObjectCRuntime.SelGetUid("addRepresentation:");
        ObjectCRuntime.ObjCMsgSend(this, selector, bitmapImageRep);
    }

    public static implicit operator IntPtr(NSImage nsApplication) =>
        nsApplication._handle;
}

internal class NSBitmapImageRep
{
    private readonly IntPtr _handle;
    public NSBitmapImageRep(
        IntPtr[] planes,
        long width,
        long height,
        long bitsPerSample,
        long samplesPerPixel,
        bool hasAlpha,
        bool isPlanar,
        NSString colorSpaceName,
        long bytesPerRow,
        long bitsPerPixel)
    {
        var nsBitmapImageRep = Appkit.ObjCGetClass("NSBitmapImageRep");
        var BitmapImageRep = ObjectCRuntime.PointerObjCMsgSend(nsBitmapImageRep, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithBitmapDataPlanes:pixelsWide:pixelsHigh:bitsPerSample:samplesPerPixel:hasAlpha:isPlanar:colorSpaceName:bytesPerRow:bitsPerPixel:");
        _handle = ObjectCRuntime.PointerObjCMsgSend(BitmapImageRep,
            selector,
            planes,
            width,
            height,
            bitsPerSample,
            samplesPerPixel,
            hasAlpha,
            isPlanar,
            colorSpaceName,
            bytesPerRow,
            bitsPerPixel);
    }

    public static implicit operator IntPtr(NSBitmapImageRep nsApplication) =>
        nsApplication._handle;
}
#endif


