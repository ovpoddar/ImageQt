using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;

internal class NSImageView : SafeHandleBaseZeroInvalid
{
    public NSImageView(CGRect cgRect) : base(true)
    {
        var nsImageView = Appkit.ObjCGetClass("NSImageView");
        var imageView = ObjectCRuntime.PointerObjCMsgSend(nsImageView, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithFrame:");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(imageView, selector, cgRect));
    }

    public void SetImage(IntPtr nsImage) => ObjectCRuntime.ObjCMsgSend(
           this,
           ObjectCRuntime.SelGetUid("setImage:"),
           nsImage);
}