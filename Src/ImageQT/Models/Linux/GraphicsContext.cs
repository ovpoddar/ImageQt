﻿#if DEBUG || Linux
using ImageQT.DllInterop.Linux;

namespace ImageQT.Models.Linux;
internal class GraphicsContext : SafeHandleZeroInvalid
{
    private readonly IntPtr _display;

    public GraphicsContext(IntPtr display, ulong window) : base(true)
    {
        _display = display;
        SetHandle(LibX11.XCreateGC(_display, window, 0, 0));
    }


    protected override bool ReleaseHandle()
    {
        LibX11.XFreeGC(_display, this.DangerousGetHandle());
        return true;
    }
}
#endif
