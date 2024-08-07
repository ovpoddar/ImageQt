﻿#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSString : SafeHandleBaseZeroInvalid
{
    public NSString() : base(true) =>
         SetHandle(IntPtr.Zero);

    public NSString(string value) : base(true)
    {
        var sringClass = ObjectCRuntime.ObjCGetClass("NSString");
        var utf8Selector = ObjectCRuntime.SelGetUid("stringWithUTF8String:");
        var title = ObjectCRuntime.PointerObjCMsgSend(sringClass, utf8Selector, value);
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(title, PreSelector.Retain));
    }
}
#endif
