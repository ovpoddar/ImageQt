#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;

internal class NSImageView : SafeHandleBaseZeroInvalid
{
    public NSImageView(CGRect cgRect) : base(true)
    {
        var nsImageView = Appkit.ObjCGetClass("NSImageView");
        var imageView = ObjectCRuntime.PointerObjCMsgSend(nsImageView, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithFrame:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(imageView, selector, cgRect));
    }

    public void SetImage(IntPtr nsImage)
    {
        var selector = ObjectCRuntime.SelGetUid("setImage:");
        ObjectCRuntime.ObjCMsgSend(this, selector, nsImage);
    }
}
#endif