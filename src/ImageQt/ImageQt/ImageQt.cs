using ImageQt.Handler;
using System.Runtime.InteropServices;

namespace ImageQt;

public class ImageQt : IDisposable
{
    private bool _disposed;
    private IntPtr _window;
    private readonly IWindow _display;

    public ImageQt(string windowTitle)
    {
        _display = new Windows();
        _window = _display.DeclareWindow(windowTitle, 200, 200);
        _display.ShowWindow(_window);
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

    public void GenerateTheBitMap(int width, int height, ref byte[] bytes)
    {
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        _display.LoadBitMap(width, height, ref imageData);
    }

    public void GenerateTheBitMap(int width, int height, ref int[] bytes)
    {
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
        _display.LoadBitMap(width, height, ref imageData);
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
    ~ImageQt()
    {
        Dispose(false);
    }
}
