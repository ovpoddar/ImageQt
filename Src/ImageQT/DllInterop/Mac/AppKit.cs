using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.DllInterop.Mac;
internal partial class Appkit
{
    private const string _dllName = "/System/Library/Frameworks/AppKit.framework/AppKit";

    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);
}
