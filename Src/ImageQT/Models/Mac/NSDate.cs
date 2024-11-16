#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSDate : SafeHandleBaseZeroInvalid
{
    public NSDate() : base(true)
    {
        var nsDate = ObjectCRuntime.ObjCGetClass("NSDate");
        var distantPast = ObjectCRuntime.SelGetUid("distantPast");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsDate, distantPast));
    }
}
#endif