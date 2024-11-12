#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Models.Mac;

internal class NSCustomClass : SafeHandleBaseZeroInvalid
{
    private readonly IntPtr _customClassPointer;
    public NSCustomClass(windowWillClose actionDelegate) : base(true)
    {
        //TODO:INVISTAGATE ObjectCRuntime.ObjCGetClass("NSImageView") method can return the same _customClassPointer so might not need to store a pointer.
        var nsObjectClass = ObjectCRuntime.ObjCGetClass("NSObject");
        _customClassPointer = ObjectCRuntime.ObjCAllocateClassPair(nsObjectClass, "NSCustomClass", 0);
        if (nsObjectClass == IntPtr.Zero)
        {
            Debug.WriteLine("fail to create class.find a unique name for this class");
            Debug.Assert(nsObjectClass != IntPtr.Zero);
        }
        var methodSelector = ObjectCRuntime.SelGetUid("windowWillClose:");
        ObjectCRuntime.ClassAddMethod(_customClassPointer, methodSelector, actionDelegate, "V@:@");
        ObjectCRuntime.ObjCRegisterClassPair(_customClassPointer);

        var appDelegateInstance = ObjectCRuntime.PointerObjCMsgSend(_customClassPointer, PreSelector.Alloc);
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(appDelegateInstance, PreSelector.Init));
    }

    protected override bool ReleaseHandle()
    {
        var response = base.ReleaseHandle();
        if (_customClassPointer != IntPtr.Zero)
            ObjectCRuntime.ObjCDisposeClassPair(_customClassPointer);
        return response;
    }
}
#endif