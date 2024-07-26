#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

namespace ImageQT.Models.Mac;
internal class NSApplication : SafeHandleBaseZeroInvalid
{
    public NSApplication() : base(true)
    {
        var nsApplication = Appkit.ObjCGetClass("NSApplication");
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsApplication, PreSelector.Alloc));
    }
}
#endif
