using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.Models.Mac;
internal class NSCustomClass
{
    private readonly IntPtr _handle;
    public NSCustomClass()
    {
        var nsObject = ObjectCRuntime.ObjCGetClass("NSObject");
        _handle = ObjectCRuntime.ObjCAllocateClassPair(nsObject, nameof(NSCustomClass), 0);
        if (_handle == IntPtr.Zero)
            throw new Exception("Could Not register my custom class");
    }

    public WindowWillClose WindowWillClose
    {
        set
        {
            var windowWillCloseSelector = ObjectCRuntime.SelGetUid("WindowWillClose:");
            ObjectCRuntime.ClassAddMethod(_handle, windowWillCloseSelector, value, "V@:@");
        }
    }

    public void RegisterCustomClass() =>
        ObjectCRuntime.ObjCRegisterClassPair(_handle);

    public void UnregisterCustomClass() =>
    ObjectCRuntime.ObjCDisposeClassPair(_handle);

    public static implicit operator IntPtr(NSCustomClass customClass) =>
        customClass._handle;
}
