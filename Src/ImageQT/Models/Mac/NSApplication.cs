using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class NSApplication : SafeHandleBaseZeroInvalid
{
    public NSApplication() : base(true)
    {
        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsApplication, PreSelector.Alloc));
    }
}
