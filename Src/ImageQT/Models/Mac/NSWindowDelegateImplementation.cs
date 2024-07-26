#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System.Diagnostics;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Models.Mac;
internal class NSWindowDelegateImplementation : SafeHandleBaseZeroInvalid
{
    private readonly IntPtr _customeClassPointer;
    public NSWindowDelegateImplementation(windowWillClose actionDelegate) : base(true)
    {
        var nsObjectClass = ObjectCRuntime.ObjCGetClass("NSObject");
        _customeClassPointer = ObjectCRuntime.ObjCAllocateClassPair(nsObjectClass, Guid.NewGuid().ToString(), 0);
        if (nsObjectClass == IntPtr.Zero)
        {
            Debug.WriteLine("fail to create class.find a unique name for this class");
            Debug.Assert(nsObjectClass != IntPtr.Zero);
        }
        var methodSelector = ObjectCRuntime.SelGetUid("windowWillClose:");
        ObjectCRuntime.ClassAddMethod(_customeClassPointer, methodSelector, actionDelegate, "V@:@");
        ObjectCRuntime.ObjCRegisterClassPair(_customeClassPointer);

        var appDelegateInstance = ObjectCRuntime.PointerObjCMsgSend(_customeClassPointer, PreSelector.Alloc);
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(appDelegateInstance, PreSelector.Init));
    }

    protected override bool ReleaseHandle()
    {
        var responce = base.ReleaseHandle();
        if (_customeClassPointer != IntPtr.Zero)
            ObjectCRuntime.ObjCDisposeClassPair(_customeClassPointer);
        return responce;
    }
}
#endif
