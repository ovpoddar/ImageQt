using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQt;

public class ImageQt : IDisposable
{
#if Windows
    private bool _disposed;
    private WndProc.WndProcDelegate _wndProcDelegate;
    private IntPtr _window;
    private IntPtr _imagePixeldata;
    private BitmapInfo _imageData;
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
        IntPtr instance = Kernel.GetModuleHandle(null);

        _wndProcDelegate = CustomWndProc;
        WindowStruct wc = new()
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
                LoadImageFromData(hWnd);
                return 0;
        }


        return Win.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }

    private void LoadImageFromData(IntPtr hWnd)
    {
        if (_imagePixeldata == IntPtr.Zero) return;
        IntPtr currentDeviceContext = Win.GetDC(hWnd);
        GDI.StretchDIBits(currentDeviceContext,
            0,
            0,
            _imageData.biWidth,
            -1 * _imageData.biHeight,
            0,
            0,
            _imageData.biWidth,
            -1 * _imageData.biHeight,
            _imagePixeldata,
            ref _imageData,
            ColorUsage.DIB_RGB_COLORS,
            DropType.SrcCopy);
        Win.ReleaseDC(hWnd, currentDeviceContext);
    }

    public void GenerateTheBitMap(int width, int height, ref byte[] bytes)
    {
        BitmapInfo bitmapInfo = new();
        bitmapInfo.biSize = Marshal.SizeOf<BitmapInfo>();
        bitmapInfo.biWidth = width;
        bitmapInfo.biHeight = -height;
        bitmapInfo.biPlanes = 1;
        bitmapInfo.biBitCount = 32;
        bitmapInfo.biCompression = 0;
        _imageData = bitmapInfo;
        _imagePixeldata = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
    }

    public void GenerateTheBitMap(int width, int height, ref int[] bytes)
    {
        BitmapInfo bitmapInfo = new();
        bitmapInfo.biSize = Marshal.SizeOf<BitmapInfo>();
        bitmapInfo.biWidth = width;
        bitmapInfo.biHeight = -height;
        bitmapInfo.biPlanes = 1;
        bitmapInfo.biBitCount = 32;
        bitmapInfo.biCompression = 0;
        _imageData = bitmapInfo;
        _imagePixeldata = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
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
            
            if (_imagePixeldata != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_imagePixeldata);
                _imagePixeldata = IntPtr.Zero;
            }
        }
#endif
    }
}
