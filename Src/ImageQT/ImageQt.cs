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

    public async Task Show(uint time = 0)
    {
        if(_disposed)
            throw new AlreadyExecutedException();

        await _windowManager.Show();
        _windowManager.Dispose();
        _disposed = true;
    }

}
