#if DEBUG || Windows
using ImageQT.DllInterop.Windows;
using ImageQT.Models.ImagqQT;

namespace ImageQT.Models.Windows;
internal class WindowWrapper : SafeHandleZeroInvalid
{
    private WindowWrapper() : base(true) { }

    protected override bool ReleaseHandle() =>
        User32.DestroyWindow(handle);
}
#endif