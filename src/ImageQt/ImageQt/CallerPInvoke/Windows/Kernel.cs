using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Windows;
internal static partial class Kernel
{
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf16, EntryPoint = "GetModuleHandleA")]
    public static partial nint GetModuleHandle(char[] lpModuleName);
}
