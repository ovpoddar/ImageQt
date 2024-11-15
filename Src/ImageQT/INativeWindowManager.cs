using ImageQT.Models.ImagqQT;

namespace ImageQT;

internal interface INativeWindowManager : IDisposable
{
    void CreateWindow(uint height, uint width);
    void SetUpImage(Image image);
    Task Show(DateTime? closeTime = null);
}