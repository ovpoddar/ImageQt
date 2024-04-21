using ImageQT.DllInterop.Mac;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class SafeHandleBaseZeroInvalid : SafeHandleZeroInvalid
{
    public SafeHandleBaseZeroInvalid(bool ownsHandle) : base(ownsHandle)
    {
    }
    protected override bool ReleaseHandle()
    {
        ObjectCRuntime.ObjCMsgSend(handle, PreSelector.Release);
        return true;
    }
}
