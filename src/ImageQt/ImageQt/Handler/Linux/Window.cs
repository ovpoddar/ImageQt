﻿using ImageQt.CallerPInvoke.Linux;
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
            throw new Exception("Could not Initilized.");
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

    public void LoadBitMap(int width, int height, ref nint ImageData)
    {
        throw new NotImplementedException();
    }
}