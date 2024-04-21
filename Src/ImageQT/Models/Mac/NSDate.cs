using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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