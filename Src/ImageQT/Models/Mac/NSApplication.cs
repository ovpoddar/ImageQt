using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal static class NSApplication
{
    public static IntPtr SharedApplication
    {
        get
        {
            var nsApplication = Appkit.ObjCGetClass("NSApplication");
            var selector = ObjectCRuntime.SelGetUid("sharedApplication");
            return ObjectCRuntime.PointerObjCMsgSend(nsApplication, selector);
        }
    }

}