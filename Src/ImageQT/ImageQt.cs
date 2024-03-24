using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT;
public sealed class ImageQt : IDisposable
{
    public ImageQt(Image image)
    {

    }

    public Task Show(uint time = 0)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
