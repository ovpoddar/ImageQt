﻿
using ImageQt.CallerPInvoke.Mac;
using ImageQt.Models.Mac;
using System.Runtime.InteropServices;

namespace ImageQt.Handler.Mac;

internal class Window : IWindow
{
    private IntPtr _app;
    private IntPtr _imageView;
    public void CleanUpResources(ref nint window)
    {
        var terminate = ObjectCRuntime.SelGetUid("terminate:");
        ObjectCRuntime.ObjCMsgSend(_app, terminate);

        if (window != IntPtr.Zero)
        {
            var release = ObjectCRuntime.SelGetUid("release");
            ObjectCRuntime.VoidObjCMsgSend(window, release);
            window = IntPtr.Zero;
        }

        if (_app != IntPtr.Zero)
        {
            var release = ObjectCRuntime.SelGetUid("release");
            ObjectCRuntime.VoidObjCMsgSend(_app, release);
            _app = IntPtr.Zero;
        }
    }

    public nint DeclareWindow(string windowTitle, uint height, uint width)
    {
        InitilizedApplication();

        var setActivationPolicy = ObjectCRuntime.SelGetUid("setActivationPolicy:");
        ObjectCRuntime.ObjCMsgSend(_app, setActivationPolicy, 0);

        var nsWindow = Appkit.ObjCGetClass("NSWindow");
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

        var nsImageView = Appkit.ObjCGetClass("NSImageView");
        nsImageView = ObjectCRuntime.ObjCMsgSend(nsImageView, alloc);
        var selInitWithFrame_Handle = ObjectCRuntime.SelRegisterName("initWithFrame:");
        _imageView = ObjectCRuntime.ObjCMsgSend(nsImageView, selInitWithFrame_Handle, cGRect);

        return window;
    }

    public void LoadBitMap(int width, int height, ref nint ImageData, IntPtr display)
    {
        var nsImage = CreateNSImage(width, height, ImageData);

        var selSetImage_Handle = ObjectCRuntime.SelRegisterName("setImage:");
        ObjectCRuntime.VoidObjCMsgSend(_imageView, selSetImage_Handle, nsImage);

        var selContentViewHandle = ObjectCRuntime.SelRegisterName("contentView");
        var contentView = ObjectCRuntime.ObjCMsgSend(display, selContentViewHandle);

        var selAddSubview_Handle = ObjectCRuntime.SelRegisterName("addSubview:");
        ObjectCRuntime.VoidObjCMsgSend(contentView, selAddSubview_Handle, _imageView);
    }

    private IntPtr CreateNSImage(int width, int height, nint imageData)
    {
        var profileNameString = ObjectCRuntime.ObjCGetClass("NSString");
        var utfString = ObjectCRuntime.SelGetUid("stringWithUTF8String:");
        var profileName = ObjectCRuntime.ObjCMsgSend(profileNameString, utfString, "NSDeviceRGBColorSpace");

        var bitmapImageRepClass = Appkit.ObjCGetClass("NSBitmapImageRep");
        var alloc = ObjectCRuntime.SelGetUid("alloc");
        var bitmapImageRep = ObjectCRuntime.ObjCMsgSend(bitmapImageRepClass, alloc);

        var planes = ObjectCRuntime.SelGetUid("initWithBitmapDataPlanes:pixelsWide:pixelsHigh:bitsPerSample:samplesPerPixel:hasAlpha:isPlanar:colorSpaceName:bytesPerRow:bitsPerPixel:");
        bitmapImageRep = ObjectCRuntime.ObjCMsgSend(
            bitmapImageRep,
            planes,
            new IntPtr[] { imageData },
            width,
            height,
            8,
            4,
            true,
            false,
            profileName,
            width * 4,
            32);

        var nsImage = Appkit.ObjCGetClass("NSImage");
        nsImage = ObjectCRuntime.ObjCMsgSend(nsImage, alloc);
        var sizeInit = ObjectCRuntime.SelGetUid("initWithSize:");
        nsImage = ObjectCRuntime.ObjCMsgSend(nsImage, sizeInit, new CGSize(width, height));
        var represention = ObjectCRuntime.SelGetUid("addRepresentation:");
        ObjectCRuntime.VoidObjCMsgSend(nsImage, represention, bitmapImageRep);

        return nsImage;
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


    [DllImport("/Users/ayan/Desktop/ImageQt/src/ImageQt/LibrarySource/Mac/arm.dylib", EntryPoint = "CreateImageWithHeightWidth")]
    public static extern IntPtr TestOP(IntPtr receiver, IntPtr selector, IntPtr arg1, long arg2, long arg3, long arg4, long arg5, [MarshalAs(UnmanagedType.Bool)] bool arg6, [MarshalAs(UnmanagedType.Bool)] bool arg7, IntPtr arg8, long arg9, long arg10);



    [DllImport("/Users/ayan/Desktop/ImageQt/src/ImageQt/LibrarySource/Mac/arm.dylib", EntryPoint = "CreateImageWithHeightWidth1")]
    public static extern IntPtr TestOPFinal(IntPtr receiver, IntPtr selector, IntPtr arg1, long arg2, long arg3, long arg4, long arg5, [MarshalAs(UnmanagedType.Bool)] bool arg6, [MarshalAs(UnmanagedType.Bool)] bool arg7, IntPtr arg8, long arg9, long arg10);

    private void InitilizedApplication()
    {
        if (_app != IntPtr.Zero)
            return;

        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        var sharedApplication = ObjectCRuntime.SelGetUid("sharedApplication");
        _app = ObjectCRuntime.ObjCMsgSend(nsApplication, sharedApplication);

    }
}
