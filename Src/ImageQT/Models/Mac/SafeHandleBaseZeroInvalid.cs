﻿#if DEBUG || OSX
using ImageQT.DllInterop.Mac;

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
#endif
