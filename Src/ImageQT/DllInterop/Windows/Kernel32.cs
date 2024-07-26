#if DEBUG || Windows
using System.Runtime.InteropServices;

namespace ImageQT.DllInterop.Windows;
internal static partial class Kernel32
{
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf16)]
    public static partial nint GetModuleHandleA([In] char[]? lpModuleName);
}
#endif