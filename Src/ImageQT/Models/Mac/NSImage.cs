#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSImage : SafeHandleBaseZeroInvalid
{
    public NSImage(CGSize size) : base(true)
    {
        var nsImage = Appkit.ObjCGetClass("NSImage");
        var image = ObjectCRuntime.PointerObjCMsgSend(nsImage, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithSize:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(image, selector, size));
    }
}
#endif