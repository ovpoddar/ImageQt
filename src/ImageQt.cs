using ImageQt.Handler;
using System.Runtime.InteropServices;

namespace ImageQt;

public class ImageQt : IDisposable
{
    private bool _disposed;
    private IntPtr _window;
    private readonly IWindow _display;

    public ImageQt(string windowTitle, Image image)
    {
        _display = new Windows();
        _window = _display.DeclareWindow(windowTitle, image.height, image.width);
        _display.ShowWindow(_window);
        if (!image.hasPath)
            _display.LoadBitMap((int)image.width, (int)image.height, ref image.imageData, _window);
        // TODO: Load it from file
    }

    public Task Run(bool isBlockCurrentThread = false)
    {
        if (_window == IntPtr.Zero)
            return Task.CompletedTask;

        if (isBlockCurrentThread)
        {
            _display.ProcessEvent(_window);
        }
        else
        {
            _ = Task.Run(() => _display.ProcessEvent(_window));
        }
        Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            // Dispose unmanaged resources
            _display.CleanUpResources(ref _window);
            _disposed = true;
        }
    }
}
