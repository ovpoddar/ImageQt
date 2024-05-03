using ImageQT.DllInterop.Linux;
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
    private ulong _window;
    private IntPtr _display;
    public nint CreateWindow(uint height, uint width)
    {
        _display = LibX11.XOpenDisplay(null);
        var screen = LibX11.XDefaultScreen(_display);
        _window = LibX11.XCreateSimpleWindow(_display,
           LibX11.XRootWindow(_display, screen),
           0,
           0,
           width,
           height,
           0,
           LibX11.XBlackPixel(_display, screen),
           LibX11.XWhitePixel(_display, screen));

        LibX11.XSelectInput(_display, _window, EventMask.ExposureMask);
        var atomDelete = LibX11.XInternAtom(_display, "WM_DELETE_WINDOW", false);
        
        IntPtr protocolsPtr = Marshal.AllocHGlobal(IntPtr.Size);
        Marshal.WriteIntPtr(protocolsPtr, (IntPtr)atomDelete);
        LibX11.XSetWMProtocols(_display, _window, protocolsPtr, 1);
        Marshal.FreeHGlobal(protocolsPtr);
        return IntPtr.Zero;
    }

    public void Dispose()
    {

    }

    public Task Show()
    {
        LibX11.XMapWindow(_display, _window);

        var ev = Marshal.AllocHGlobal(192);
        // var message = LibX11.XInternAtom(_display, "WM_PROTOCOLS", true);

        while (true)
        {
           LibX11.XNextEvent(_display, ev);
           var @event = new XEvent(ref ev);
           if (@event.type == Event.ClientMessage
            //    && @event.xclient.message_type == message
               )
           {
               break;
           }
        }
        Marshal.FreeHGlobal(ev);
        return Task.CompletedTask;
    }
}
