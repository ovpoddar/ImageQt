using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Mac;
public partial class Appkit
{
    private const string _dllName = "/System/Library/Frameworks/AppKit.framework/AppKit";

    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);

}
