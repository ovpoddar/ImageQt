using System.Runtime.InteropServices;

namespace ImageQt.Handler;

internal interface IWindow
{
    void CleanUpResources(GCHandle window);
    nint DeclareWindow(string windowTitle, uint height, uint width);
    void ProcessEvent(nint window);
    void ShowWindow(nint window);
}