#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSDate : SafeHandleBaseZeroInvalid
{
    public NSDate() : base(true) =>
        SetHandle(IntPtr.Zero);

    public NSDate DistantPast
    {
        get
        {
            var nsDateClass = ObjectCRuntime.ObjCGetClass("NSDate");
            var selector = ObjectCRuntime.SelGetUid("distantPast");
            var distantPast = ObjectCRuntime.PointerObjCMsgSend(nsDateClass, selector);
            SetHandle(ObjectCRuntime.PointerObjCMsgSend(distantPast, PreSelector.Retain));
            return this;
        }
    }
}
#endif
