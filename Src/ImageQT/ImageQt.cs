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
        _windowManager = new Handlers.Window.WindowManager();
        _windowManager.CreateWindow((uint)image.Height, (uint)image.Width);
        _windowManager.SetUpImage(image);
    }

    public async Task Show(TimeSpan? time = null)
    {
        if(_disposed)
            throw new AlreadyExecutedException();

        await _windowManager.Show(time.HasValue ? DateTime.Now.Add(time.Value) : null);
        _windowManager.Dispose();
        _disposed = true;
    }

}
