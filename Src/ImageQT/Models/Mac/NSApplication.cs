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
        ObjectCRuntime.BoolObjCMsgSend(this, ObjectCRuntime.SelGetUid("setActivationPolicy:"), value);

    public static implicit operator IntPtr(NSApplication nsApplication) =>
        nsApplication._handle;

    // TODO: can be trimmed for release.
    public void ActivateIgnoringOtherApps(bool ignoreOtherApps)
    {
        if (OperatingSystem.IsMacCatalystVersionAtLeast(14, 10))
        {
            ObjectCRuntime.ObjCMsgSend(
             this,
             ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:"),
             ignoreOtherApps);
        }
        else
        {
            // TODO verify on latest m2
            ObjectCRuntime.ObjCMsgSend(
                this,
                ObjectCRuntime.SelGetUid("activate"));
        }
    }
}