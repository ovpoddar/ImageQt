using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.DllInterop.Mac;
internal static class PreSelector
{
    public static IntPtr Retain => ObjectCRuntime.SelGetUid("retain");
    public static IntPtr Release => ObjectCRuntime.SelGetUid("release");
    public static IntPtr Alloc => ObjectCRuntime.SelGetUid("alloc");
    public static IntPtr Init => ObjectCRuntime.SelGetUid("init");
}

