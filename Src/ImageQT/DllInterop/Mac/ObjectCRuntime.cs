#if DEBUG || OSX
using ImageQT.Models.Mac;
using System.Drawing;
using System.Runtime.InteropServices;
using static ImageQT.Models.Mac.WindowDelegate;

namespace ImageQT.DllInterop.Mac;
internal partial class ObjectCRuntime
{
    private const string _dllName = "libobjc";

    [LibraryImport(_dllName, EntryPoint = "sel_getUid")]
    public static partial IntPtr SelGetUid([MarshalAs(UnmanagedType.LPStr)] string selectorName);



    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void ObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void ObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void ObjCMsgSend(IntPtr receiver, IntPtr selector, NSCustomClass arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial void ObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.Bool)] bool arg1);



    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, CGSize arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, CGRect arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.LPStr)] string arg1);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, CGRect arg1, long arg2, long arg3, [MarshalAs(UnmanagedType.Bool)] bool arg4);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, ulong arg1, NSDate arg2, NSString arg3, [MarshalAs(UnmanagedType.Bool)] bool arg4);

    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    public static partial IntPtr PointerObjCMsgSend(IntPtr receiver, IntPtr selector, IntPtr[] arg1, long arg2, long arg3, long arg4, long arg5, [MarshalAs(UnmanagedType.Bool)] bool arg6, [MarshalAs(UnmanagedType.Bool)] bool arg7, NSString arg8, long arg9, long arg10);



    [LibraryImport(_dllName, EntryPoint = "objc_msgSend")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool BoolObjCMsgSend(IntPtr receiver, IntPtr selector, int arg1);



    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);



    [LibraryImport(_dllName, EntryPoint = "objc_allocateClassPair")]
    public static partial IntPtr ObjCAllocateClassPair(IntPtr classSelector, [MarshalAs(UnmanagedType.LPStr)] string selectorName, ulong extraAllocate);



    [LibraryImport(_dllName, EntryPoint = "objc_registerClassPair")]
    public static partial void ObjCRegisterClassPair(IntPtr customClass);



    [LibraryImport(_dllName, EntryPoint = "objc_disposeClassPair")]
    public static partial void ObjCDisposeClassPair(IntPtr customClass);



    [LibraryImport(_dllName, EntryPoint = "class_addMethod")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ClassAddMethod(IntPtr customClass,
        IntPtr name,
        windowWillClose imp,
        [MarshalAs(UnmanagedType.LPStr)] string types);
}
#endif