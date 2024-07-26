#if DEBUG || OSX
using System.Runtime.InteropServices;

namespace ImageQT.DllInterop.Mac;
internal partial class Appkit
{
    private const string _dllName = "/System/Library/Frameworks/AppKit.framework/AppKit";

    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);
}
#endif