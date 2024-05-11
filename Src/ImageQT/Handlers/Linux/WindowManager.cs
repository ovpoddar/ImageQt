using ImageQT.DllInterop.Linux;
using ImageQT.Models.ImagqQT;
using ImageQT.Models.Linux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Handlers.Linux;
internal class WindowManager : INativeWindowManager
{
    private readonly IntPtr _display;
    private readonly int _screen;
    private readonly ulong _atomDelete;
    private ulong? _window;
    private IntPtr? _image;
    private ulong? _pixmap;

    public WindowManager()
    {
        _display = LibX11.XOpenDisplay(null);
        _atomDelete = LibX11.XInternAtom(_display, "WM_DELETE_WINDOW", false);
        _screen = LibX11.XDefaultScreen(_display);
    }

    public nint CreateWindow(uint height, uint width)
    {
        _window = LibX11.XCreateSimpleWindow(_display,
           LibX11.XRootWindow(_display, _screen),
           0,
           0,
           width,
           height,
           0,
           LibX11.XBlackPixel(_display, _screen),
           LibX11.XWhitePixel(_display, _screen));

        LibX11.XSelectInput(_display, _window.Value, EventMask.ExposureMask);

        var atomPointer = Marshal.AllocHGlobal(IntPtr.Size);
        Marshal.WriteIntPtr(atomPointer, (IntPtr)_atomDelete);
        LibX11.XSetWMProtocols(_display, _window.Value, atomPointer, 1);
        Marshal.FreeHGlobal(atomPointer);

        return IntPtr.Zero;
    }

    public void Dispose()
    {
        if (!_window.HasValue)
            return;

        if (_pixmap.HasValue)
            LibX11.XFreePixmap(_display, _pixmap.Value);

        LibX11.XDestroyWindow(_display, _window.Value);
    }

    public Task Show()
    {
        if (!_window.HasValue || !_image.HasValue|| !_pixmap.HasValue)
            return Task.CompletedTask;

        LibX11.XMapWindow(_display, _window.Value);

        var image = Marshal.PtrToStructure<XImage>(_image.Value);
        var ev = Marshal.AllocHGlobal(192);
        var graphicsContext = LibX11.XCreateGC(_display, _window.Value, 0, 0);

        while (true)
        {
            LibX11.XNextEvent(_display, ev);
            var @event = new XEvent(ref ev);
            if (@event.type == Event.ClientMessage
                 && @event.xclient.data.l == (int)_atomDelete)
            {
                LibX11.XCloseDisplay(_display);
                break;
            }
            else
            {
                LibX11.XPutImage(_display, _pixmap.Value, graphicsContext, _image.Value, 0, 0, 0, 0, (uint)image.width, (uint)image.height);
                LibX11.XCopyArea(_display, _pixmap.Value, _window.Value, graphicsContext, 0, 0, (uint)image.width, (uint)image.height, 0, 0);
            }
        }

        LibX11.XFreeGC(_display, graphicsContext);
        Marshal.FreeHGlobal(ev);
        return Task.CompletedTask;
    }

    public void SetUpImage(Image image)
    {
        if (!_window.HasValue)
            return;

        var visual = LibX11.XDefaultVisual(_display, _screen);
        var depth = LibX11.XDefaultDepth(_display, _screen);

        _image = LibX11.XCreateImage(_display,
            visual, 
            depth, 
            ImageFormat.ZPixmap, 
            0, 
            image.Id,
            (uint)image.Width, 
            (uint)image.Height,
            image.BitCount,
            0);
        _pixmap = LibX11.XCreatePixmap(_display,
            _window.Value, 
            (uint)image.Width, 
            (uint)image.Height, 
            depth);
    }
}
