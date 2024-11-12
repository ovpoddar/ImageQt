#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class CustomMethodDelegate : SafeHandleBaseZeroInvalid
{
    public CustomMethodDelegate(IntPtr targetClass) : base(true)
    {
        var nsCustomCLass = ObjectCRuntime.PointerObjCMsgSend(targetClass, PreSelector.Alloc);
        SetHandle(ObjectCRuntime.PointerObjCMsgSend(nsCustomCLass, PreSelector.Init));
    }
}
#endif