#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal static class NSContentView
{
    public static void AddSubview(this IntPtr contentView, NSImageView nsImageView)
    {
        var selector = ObjectCRuntime.SelGetUid("addSubview:");
        ObjectCRuntime.ObjCMsgSend(contentView, selector, nsImageView);
    }
}
#endif