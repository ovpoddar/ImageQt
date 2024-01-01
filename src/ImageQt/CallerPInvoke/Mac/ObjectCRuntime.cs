using ImageQt.Models.Mac;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Mac;
public partial class ObjectCRuntime
{

    private const string _dllName = "libobjc";


    [LibraryImport(_dllName, EntryPoint = "sel_registerName")]
    public static partial IntPtr SelRegisterName([MarshalAs(UnmanagedType.LPStr)] string name);


    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);


    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void VoidObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void VoidObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);


    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ObjCMsgSend(IntPtr receiver, IntPtr selector, int arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, CGRect arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, CGSize arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.Bool)] bool arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.LPStr)] string arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, CGRect arg1, NSWindowStyleMask arg2, NSBackingStore arg3, [MarshalAs(UnmanagedType.Bool)] bool arg4);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr ObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr[] arg1, long arg2, long arg3, long arg4, long arg5, [MarshalAs(UnmanagedType.Bool)] bool arg6, [MarshalAs(UnmanagedType.Bool)] bool arg7, IntPtr arg8, long arg9, long arg10);
    
   
    [LibraryImport(_dllName, EntryPoint = "sel_getUid")]
    public static partial IntPtr SelGetUid([MarshalAs(UnmanagedType.LPStr)] string selectorName);

}
