using ImageQt.CallerPInvoke.Linux;
using ImageQt.Models.Linux;
using ImageQt.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQt.Handler.Linux;

internal class Window : IWindow
{
    private ulong _x11Window;

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

        XLib.XSelectInput(display, _x11Window, EventMask.EnterWindowMask);
        return display;
    }

    public void ShowWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
            throw new Exception("Could not Initialize.");
        XLib.XMapWindow(window, _x11Window);
    }

    public void ProcessEvent(IntPtr window)
    {
        var ev = Marshal.AllocHGlobal(192);
        while(true)
        {
            XLib.XNextEvent(window, ev);

            var @event = new XEvent(ref ev);
            if (@event.type == Event.DestroyNotify)
            {
                Marshal.FreeHGlobal(ev);
                break;
            }
        }

    }

    public void CleanUpResources(ref IntPtr window)
    {
        if (window == IntPtr.Zero)
            return;

        XLib.XCloseDisplay(window);
        window = IntPtr.Zero;
    }

    public void LoadBitMap(int width, int height, ref nint imageData)
    {
        var graphicsContext = XLib.XCreateGC()
        /*
            GC gc = XCreateGC(display, window, 0, NULL);
           // Create an XImage from the pixel data
           XImage *image = XCreateImage(display, DefaultVisual(display, screen), DefaultDepth(display, screen),
                ZPixmap, 0, memory.get(), width, height, 32, 0);
           Pixmap pixmap = XCreatePixmap(display, window, width, height, DefaultDepth(display, screen));
           
           XPutImage(display, pixmap, gc, image, 0, 0, 0, 0, width, height);
           
           XCopyArea(display, pixmap, window, gc, 0, 0, width, height, 0, 0);
           XFreePixmap(display, pixmap);
           // XDestroyImage(image);
           XFreeGC(display, gc);
         */
    }
}