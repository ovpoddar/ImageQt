using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
