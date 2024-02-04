using ImageQt.CallerPInvoke.Linux;
using ImageQt.Models.Linux;
using System.Runtime.InteropServices;

namespace ImageQt.Handler.Linux;

internal class Window : IWindow
{
    private ulong _x11Window;
    private IntPtr _graphicsContext;
    private IntPtr _image;
    private IntPtr _visual;
    private uint _depth;

    public IntPtr DeclareWindow(string windowTitle, uint height, uint width)
    {
        var display = XLib.XOpenDisplay(null);
        var screen = XLib.XDefaultScreen(display);
        _x11Window = XLib.XCreateSimpleWindow(display,
            XLib.XRootWindow(display, screen),
            0,
            0,
            width,
            height,
            0,
            XLib.XBlackPixel(display, screen),
            XLib.XWhitePixel(display, screen));
        _graphicsContext = XLib.XCreateGC(display, _x11Window, 0, 0);
        _visual = XLib.DefaultVisual(display, screen);
        _depth = XLib.DefaultDepth(display, screen);
        XLib.XStoreName(display, _x11Window, windowTitle);
        XLib.XSelectInput(display, _x11Window, EventMask.ButtonPressMask | EventMask.ExposureMask);
        return display;
    }

    public void ShowWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
            throw new Exception("Could not Initialize.");
        XLib.XMapWindow(window, _x11Window);
    }
    public void LoadBitMap(int width, int height, ref nint imageData, IntPtr display)
    {
        _image = XLib.XCreateImage(display, _visual, _depth, ImageFormat.ZPixmap, 0, imageData, (uint)width, (uint)height, 32, 0);
    }
    public void ProcessEvent(IntPtr window)
    {
        var ev = Marshal.AllocHGlobal(192);
        while (true)
        {
            XLib.XNextEvent(window, ev);

            var @event = new XEvent(ref ev);
            if (@event.type == Event.ButtonPress)
            {
                XLib.XCloseDisplay(window);
                Marshal.FreeHGlobal(ev);
                break;
            }

            if (@event.type == Event.Expose)
            {
                DrawImageFromPointer(window);
            }
        }

    }
    void DrawImageFromPointer(IntPtr display)
    {
        if (_image == IntPtr.Zero
            || display == IntPtr.Zero)
            return;
        var image = Marshal.PtrToStructure<XImage>(_image);

        XLib.XPutImage(display, _x11Window, _graphicsContext, _image, 0, 0, 0, 0, (uint)image.width, (uint)image.height);
    }
    public void CleanUpResources(ref IntPtr window)
    {
        if (_graphicsContext != default)
            XLib.XFreeGC(window, _graphicsContext);

        if (window != IntPtr.Zero)
            XLib.XDestroyWindow(window, _x11Window);

    }


}