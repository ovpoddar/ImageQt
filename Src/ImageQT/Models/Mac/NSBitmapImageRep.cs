#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
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