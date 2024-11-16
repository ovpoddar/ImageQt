#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System.Diagnostics;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Models.Mac;

internal class NSCustomClass : SafeHandleBaseZeroInvalid
{
    public NSCustomClass(WindowWillClose actionDelegate) : base(true)
    {
        var nsObjectClass = ObjectCRuntime.ObjCGetClass("NSObject");
        var nsCustomClass = ObjectCRuntime.ObjCAllocateClassPair(nsObjectClass, "NSCustomClass", 0);
        if (nsObjectClass == IntPtr.Zero)
        {
            Debug.WriteLine("fail to create class.find a unique name for this class");
            Debug.Assert(nsObjectClass != IntPtr.Zero);
        }
        var methodSelector = ObjectCRuntime.SelGetUid("WindowWillClose:");
        ObjectCRuntime.ClassAddMethod(nsCustomClass, methodSelector, actionDelegate, "V@:@");
        ObjectCRuntime.ObjCRegisterClassPair(nsCustomClass);

        var appDelegateInstance = ObjectCRuntime.PointerObjCMsgSend(nsCustomClass, PreSelector.Alloc);
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(appDelegateInstance, PreSelector.Init));
    }

    protected override bool ReleaseHandle()
    {
        // do not rearrange order matter here.
        var response = base.ReleaseHandle();
        var nsCustomClass = ObjectCRuntime.ObjCGetClass("NSCustomClass");
        ObjectCRuntime.ObjCDisposeClassPair(nsCustomClass);
        return response;
    }
}
#endif