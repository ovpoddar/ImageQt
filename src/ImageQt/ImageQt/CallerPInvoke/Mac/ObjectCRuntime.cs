using ImageQt.Models.Mac;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Mac;
public partial class ObjectCRuntime
{

    private const string _dllName = "libobjc";

    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);


    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void Void_ObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.Bool)] bool arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.LPStr)] string arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, int arg1);

    [LibraryImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, CGRect arg1, NSWindowStyleMask arg2, NSBackingStore arg3, [MarshalAs(UnmanagedType.Bool)] bool arg4);

    
    [LibraryImport(_dllName, EntryPoint = "sel_getUid")]
    public static partial IntPtr SelGetUid([MarshalAs(UnmanagedType.LPStr)] string selectorName);

}
