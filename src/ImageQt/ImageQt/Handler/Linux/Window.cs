using ImageQt.CallerPInvoke.Linux;

namespace ImageQt.Handler.Linux;

internal class Window
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

        XLib.XSelectInput(display, _x11Window, 4);
        return display;
    }

    public void ShowWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
            throw new Exception("Could not Initilized.");
        XLib.XMapWindow(window, _x11Window);
    }
}