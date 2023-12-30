using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.CallerPInvoke.Mac;
public partial class FoundationFramework
{
    private const string _dllName = "/System/Library/Frameworks/Foundation.framework/Foundation";

    [LibraryImport(_dllName, EntryPoint = "objc_getClass")]
    public static partial IntPtr ObjCGetClass([MarshalAs(UnmanagedType.LPStr)] string name);

}
