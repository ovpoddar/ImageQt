using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class NSWindow : SafeHandleBaseZeroInvalid
{
    public NSWindow() : base(true)
    {
        var nsWindow = Appkit.ObjCGetClass("NSWindow");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsWindow, PreSelector.Alloc));
    }
}