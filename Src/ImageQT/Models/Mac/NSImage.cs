using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class NSImage
{
    private readonly IntPtr _handle;
    public NSImage(CGSize size)
    {
        var nsImage = Appkit.ObjCGetClass("NSImage");
        var image = ObjectCRuntime.PointerObjCMsgSend(nsImage, PreSelector.Alloc);
        var selector = ObjectCRuntime.SelGetUid("initWithSize:");
        _handle = ObjectCRuntime.PointerObjCMsgSend(image, selector, size);
    }

    public void AddRepresentation(NSBitmapImageRep bitmapImageRep)
    {
        var selector = ObjectCRuntime.SelGetUid("addRepresentation:");
        ObjectCRuntime.ObjCMsgSend(this, selector, bitmapImageRep);
    }

    public static implicit operator IntPtr(NSImage nsApplication) =>
        nsApplication._handle;
}
