using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XExtension
{
    public _XExtension* Next;
    public XExtCodes Codes;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> CreateGC;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> CopyGC;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> FlushGC;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> FreeGC;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> CreateFont;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int> FreeFont;
    public delegate* unmanaged[Cdecl]<XDisplay*, XExtCodes*, int> CloseDisplay;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, XExtCodes*, int*, int> Error;
    public delegate* unmanaged[Cdecl]<XDisplay*, int, XExtCodes*, IntPtr, int, IntPtr> ErrorString;
    public IntPtr Name;
    public delegate* unmanaged[Cdecl]<XDisplay*, XErrorEvent*, IntPtr, void> ErrorValues;
    public delegate* unmanaged[Cdecl]<XDisplay*, XExtCodes*, IntPtr, long, void> BeforeFlush;
    public _XExtension* NextFlush;
    public string? GetName() =>
        Marshal.PtrToStringAnsi(Name);
}
