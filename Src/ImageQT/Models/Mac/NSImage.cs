using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class NSImage : SafeHandleBaseZeroInvalid
{
    public NSImage(CGSize size) : base(true)
    {
        var nsImage = Appkit.ObjCGetClass("NSImage");
        var image = ObjectCRuntime.PointerObjCMsgSend(nsImage, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithSize:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(image, selector, size));
    }
}
