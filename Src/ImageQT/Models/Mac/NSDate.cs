#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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