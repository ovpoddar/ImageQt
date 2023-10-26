using System.Runtime.InteropServices;

namespace ImageQt.Handler.Mac;

internal class Window : IWindow
{
    public void CleanUpResources(GCHandle window)
    {
        throw new PlatformNotSupportedException();
    }

    public nint DeclareWindow(string windowTitle, uint height, uint width)
    {
        throw new PlatformNotSupportedException();
    }

    public void ProcessEvent(nint window)
    {
        throw new PlatformNotSupportedException();
    }

    public void ShowWindow(nint window)
    {
        throw new PlatformNotSupportedException();
    }
}
