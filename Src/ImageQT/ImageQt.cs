using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageQT.Exceptions;
using ImageQT.Models.ImagqQT;

namespace ImageQT;
public class ImageQt
{
    private bool _disposed;
    private INativeWindowManager _windowManager;

    public ImageQt(Image image)
    {
#if DEBUG
        if (OperatingSystem.IsWindows())
            _windowManager = new Handlers.Window.WindowManager();
        else if (OperatingSystem.IsLinux())
            _windowManager = new Handlers.Linux.WindowManager();
        else if (OperatingSystem.IsMacCatalyst())
            _windowManager = new Handlers.Mac.WindowManager();
        else
            throw new PlatformNotSupportedException();
#else
#if Windows
        _windowManager = new Handlers.Window.WindowManager();
#elif OSX
            _windowManager = new Handlers.Mac.WindowManager();
#elif Linux
            _windowManager = new Handlers.Linux.WindowManager();
#else
            throw new PlatformNotSupportedException();
#endif
#endif
        _windowManager.CreateWindow((uint)image.Height, (uint)image.Width);
        _windowManager.SetUpImage(image);
    }

    public async Task Show(TimeSpan? time = null)
    {
        if (_disposed)
            throw new AlreadyExecutedException();

        await _windowManager.Show(time.HasValue ? DateTime.Now.Add(time.Value) : null);
        _windowManager.Dispose();
        _disposed = true;
    }

}
