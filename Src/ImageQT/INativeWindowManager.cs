using ImageQT.Models.ImagqQT;

namespace ImageQT;

internal interface INativeWindowManager : IDisposable
{
    IntPtr CreateWindow(uint height, uint width);
    void SetUpImage(Image image);
    Task Show();
}