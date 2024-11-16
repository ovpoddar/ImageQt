#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSString : SafeHandleBaseZeroInvalid
{
    public NSString(string value) : base(true)
    {
        var nsString = ObjectCRuntime.ObjCGetClass("NSString");
        var stringWithUTF8String = ObjectCRuntime.SelGetUid("stringWithUTF8String:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsString, stringWithUTF8String, value));
    }
}
#endif