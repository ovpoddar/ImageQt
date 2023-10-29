using ImageQt.CallerPInvoke.Linux;
using ImageQt.Models.Linux;
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
            throw new Exception("Could not Initilized.");
        XLib.XMapWindow(window, _x11Window);
    }

    public void ProcessEvent(IntPtr window)
    {
        //var @event = IntPtr.Zero;
        IntPtr ev = Marshal.AllocHGlobal(192);

        while (true)
        {
            XLib.XNextEvent(window, ev);
            var xevent = Marshal.PtrToStructure<XEvent>(ev);
            if(xevent.type==4)
            {
                Marshal.FreeHGlobal(ev);
                return;
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
}