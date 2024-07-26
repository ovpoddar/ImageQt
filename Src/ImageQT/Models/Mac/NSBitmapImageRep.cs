#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSBitmapImageRep : SafeHandleBaseZeroInvalid
{
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
        long bitsPerPixel) : base(true)
    {
        var nsBitmapImageRep = Appkit.ObjCGetClass("NSBitmapImageRep");
        var BitmapImageRep = ObjectCRuntime.PointerObjCMsgSend(nsBitmapImageRep, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithBitmapDataPlanes:pixelsWide:pixelsHigh:bitsPerSample:samplesPerPixel:hasAlpha:isPlanar:colorSpaceName:bytesPerRow:bitsPerPixel:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(BitmapImageRep,
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
            bitsPerPixel));
    }
}
#endif
