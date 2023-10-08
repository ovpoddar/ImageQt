using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace ImageQt;

public class ImageQt : IDisposable
{
#if Windows
    private bool _disposed;
    private WndProc.WndProcDelegate _wndProcDelegate;
    private IntPtr _window;
    private IntPtr _image;
#endif


    public ImageQt(string windowTitle)
    {
#if Linux
        DeclarWindow();
        ShowWindow();
#elif OSX
        DeclarWindow();
        ShowWindow();
#elif Windows
        DeclarWindow(windowTitle);
        ShowWindow();
#endif
    }

    public Task Run(bool isBlockCurrentThread = false)
    {
        if (_window == IntPtr.Zero)
            return Task.CompletedTask;

        if (isBlockCurrentThread)
        {
            var message = new Message();

            int ret;
            while ((ret = Win.GetMessage(out message, 0, 0, 0)) != 0)
            {
                if (ret == -1)
                {
                    //-1 indicates an error
                }
                else
                {
                    Win.TranslateMessage(ref message);
                    Win.DispatchMessage(ref message);
                }
            }
        }
        else
        {
            _ = Task.Run(() =>
            {
                var message = new Message();

                int ret;
                while ((ret = Win.GetMessage(out message, 0, 0, 0)) != 0)
                {
                    if (ret == -1)
                    {
                        //-1 indicates an error
                    }
                    else
                    {
                        Win.TranslateMessage(ref message);
                        Win.DispatchMessage(ref message);
                    }
                }
            });
        }

        return Task.CompletedTask;
    }

    private void DeclarWindow(string windowTitle)
    {
        var instance = Kernel.GetModuleHandle(null);

        _wndProcDelegate = CustomWndProc;
        var wc = new WindowStruct
        {
            hInstance = instance,
            lpszClassName = windowTitle,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_wndProcDelegate)
        };

        Win.RegisterClassW(ref wc);
        _window = Win.CreateWindowExW(
            0,
            windowTitle,
            "Some Name",
            13565952,
            -2147483648,
            -2147483648,
            -2147483648,
            -2147483648,
            IntPtr.Zero,
            IntPtr.Zero,
            instance,
            IntPtr.Zero);
    }

    private nint CustomWndProc(nint hWnd, ProcessesMessage msg, nint wParam, nint lParam)
    {
        switch (msg)
        {
            case ProcessesMessage.WM_DESTROY:
                Win.PostQuitMessage(0);
                return 0;

            case ProcessesMessage.WM_PAINT:
                LoadAndDisplayBMP(hWnd);
                return 0;
        }


        return Win.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }

    private void LoadAndDisplayBMP(nint hWnd)
    {
        if (_image == IntPtr.Zero)
            return;

        var currentDeviceContext = Win.GetDC(hWnd);
        var memory = GDI.CreateCompatibleDC(currentDeviceContext);
        var bitmap = GDI.SelectObject(memory, _image);
        Bitmap newBitmap = new();
        GDI.GetObject(_image, Marshal.SizeOf<Bitmap>(), out newBitmap);
        GDI.BitBlt(currentDeviceContext,
            0,
            0,
            newBitmap.bmWidth,
            newBitmap.bmHeight, 
            memory,
            0,
            0,
            13369376);
        GDI.SelectObject(memory, bitmap);
        GDI.DeleteDC(memory);
        Win.ReleaseDC(hWnd, currentDeviceContext);
    }

    // support bmp file only.
    public Task LoadImage(string path)
    {
        _image = Win.LoadImage(IntPtr.Zero, path, 0, 0, 0, 16);

        if (_image == IntPtr.Zero)
            throw new Exception("could not load the image");

        return Task.CompletedTask;
    }

    private void ShowWindow()
    {
        if (_window == IntPtr.Zero)
            throw new Exception("Could not Initilized.");
        Win.ShowWindow(_window, 5);
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
#if Windows
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
            }

            // Dispose unmanaged resources
            if (_window != IntPtr.Zero)
            {
                Win.DestroyWindow(_window);
                _window = IntPtr.Zero;
            }
            
            if (_image != IntPtr.Zero)
            {
                GDI.DeleteObject(_image);
                _image = IntPtr.Zero;
            }
        }
#endif
    }
}
