namespace ImageQT;

internal interface INativeWindowManager : IDisposable
{
    IntPtr CreateWindow(uint height, uint width);
    Task Show();
}