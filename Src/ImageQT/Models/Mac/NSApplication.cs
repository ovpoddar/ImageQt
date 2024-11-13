using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal sealed class NSApplication
{
    private static NSApplication? _instance = null;
    private readonly IntPtr _handle;

    public NSApplication()
    {
        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        var selector = ObjectCRuntime.SelGetUid("sharedApplication");
        _handle = ObjectCRuntime.PointerObjCMsgSend(nsApplication, selector);
    }

    public static NSApplication SharedApplication => 
        _instance ??= new NSApplication();

    public bool SetSetActivationPolicy(NSApplicationActivationPolicy value) =>
        ObjectCRuntime.BoolObjCMsgSend(this._handle, ObjectCRuntime.SelGetUid("setActivationPolicy:"), value);

    public static implicit operator IntPtr(NSApplication nsApplication) =>
        nsApplication._handle;

}