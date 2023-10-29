#include <X11/Xlib.h>

extern "C"
{
    Display* XOpenDisplayX(char* displayName)
    {
        return XOpenDisplay(displayName);
    }

    int XDefaultScreenX(Display* display)
    {
        return XDefaultScreen(display);
    }

    unsigned long XCreateSimpleWindowX(Display* display,
        unsigned long window,
        int x,
        int y,
        unsigned int width,
        unsigned int height,
        unsigned int borderWidth,
        unsigned long border,
        unsigned long background)
    {
        return XCreateSimpleWindow(display, window, x, y, width, height, borderWidth, border, background);
    }

    unsigned long XRootWindowX(Display* display, int screen)
    {
        return XRootWindow(display, screen);
    }

    unsigned long XBlackPixelX(Display* display, int screen)
    {
        return XBlackPixel(display, screen);
    }

    unsigned long XWhitePixelX(Display* display, int screen)
    {
        return XWhitePixel(display, screen);
    }

    int XSelectInputX(Display* display, unsigned long window, long event_mask)
    {
        return XSelectInput(display, window, event_mask);
    }

    int XMapWindowX(Display* display, unsigned long window)
    {
        return XMapWindow(display, window);
    }

    int XNextEventX(Display* display, XEvent event)
    {
        return XNextEvent(display, &event);
    }

    int XCloseDisplayX(Display* display)
    {
        return XCloseDisplay(display);
    }
}