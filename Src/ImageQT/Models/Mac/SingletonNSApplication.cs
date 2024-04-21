using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal static class SingletonNSApplication
{
    public static IntPtr NSApplication { get; set; } = IntPtr.Zero;

    public static void NSInitialized()
    {
        if (NSApplication != IntPtr.Zero)
        {
            return;
        }
        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        var sharedApplication = ObjectCRuntime.SelGetUid("sharedApplication");
        NSApplication = ObjectCRuntime.PointerObjCMsgSend(nsApplication, sharedApplication);
        var setActivationPolicy = ObjectCRuntime.SelGetUid("setActivationPolicy:");
        ObjectCRuntime.BoolObjCMsgSend(NSApplication, setActivationPolicy, 0);
        var makeitTop = ObjectCRuntime.SelGetUid("activateIgnoringOtherApps:");
        ObjectCRuntime.ObjCMsgSend(NSApplication, makeitTop, true);
        return;
    }
}