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

    unsigned int DefaultDepthX(Display* display, int screen)
    {
        return DefaultDepth(display, screen);
    }

    Visual* DefaultVisualX(Display* display, int screen)
    {
        return DefaultVisual(display, screen);
    }

    int XSelectInputX(Display* display, unsigned long window, long event_mask)
    {
        return XSelectInput(display, window, event_mask);
    }

    int XMapWindowX(Display* display, unsigned long window)
    {
        return XMapWindow(display, window);
    }

    int XNextEventX(Display* display, XEvent* event)
    {
        return XNextEvent(display, event);
    }

    int XCloseDisplayX(Display* display)
    {
        return XCloseDisplay(display);
    }
    
    GC XCreateGCX(Display* display, ulong drawable, unsigned long valueMask, XGCValues * values)
    {
        return XCreateGC(display, drawable, valueMask, values); 
    }

    XImage* XCreateImageX(Display* display,
        Visual* visual,
        unsigned int depth,
        int format,
        int offset,
        char* data,
        unsigned int width,
        unsigned int height,
        int bitmapPad,
        int bytesPerLine)
    {
        return XCreateImage(display, visual, depth,format, offset, data, width, height, bitmapPad, bytesPerLine);
    }

    unsigned long XCreatePixmapX(Display* display,
        unsigned long window,
        unsigned int width,
        unsigned int height,
        unsigned int depth)
    {
        return XCreatePixmap(display, window, width, height, depth);
    }

    void XPutImageX(Display* display, 
        ulong drawable, 
        GC gc, 
        XImage* imageData, 
        int srcX, 
        int srcY, 
        int destX, 
        int destY, 
        unsigned int width,
        unsigned int height)
    {
        XPutImage(display, drawable, gc, imageData, srcX, srcY, destX, destY, width, height);
    }

    void XCopyAreaX(Display* display, 
        ulong src, 
        ulong dest, 
        GC gc, 
        int srcX, 
        int srcY,
        unsigned int width, 
        unsigned int height, 
        int destX, 
        int destY)
    {
        XCopyArea(display, src, dest, gc, srcX, srcY, width, height, destX, destY);
    }

    void XFreePixmapX(Display* display, ulong pixmap)
    {
        XFreePixmap(display, pixmap);
    }

    void XFreeGCX(Display* display, GC gc)
    {
        XFreeGC(display, gc);
    }

    void XDestroyWindowX(Display* display, ulong window)
    {
        XDestroyWindow(display, window);
    }
}
