namespace ImageQt;

public class ImageQt : IDisposable
{
    public ImageQt(string windowTitle, bool isBlockCurrentThread = false)
    {
#if Linux
        DeclarWindow();
        ShowWindow();
#elif OSX
        DeclarWindow();
        ShowWindow();
#elif Windows
        DeclarWindow();
        ShowWindow();
#endif
    }

    private void ShowWindow()
    {
        throw new NotImplementedException();
    }

    private void DeclarWindow()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
