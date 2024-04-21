using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageQT.Models.ImagqQT;

namespace ImageQT;
public class ImageQt : IDisposable
{
    private bool _disposed;
    private INativeWindowManager _windowManager;

    public ImageQt(Image image)
    {
        _windowManager = new Handlers.Window.WindowManager();
        _windowManager.CreateWindow((uint)image.Height, (uint)image.Width);
    }

    public async Task Show(uint time = 0)
    {
        await _windowManager.Show();
    }

    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);
        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        _windowManager.Dispose();
        _disposed = true;
    }
}
