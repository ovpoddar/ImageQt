using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ImageQt;

public class ImageQt : IDisposable
{
#if Windows
    private bool _disposed;
    private IntPtr _window;
    private IntPtr _imagePixeldata;
    private BitmapInfo _imageData;
#endif


    public ImageQt(string windowTitle)
    {
        var window = new Windows();
        _window = window.DeclareWindow(windowTitle, 200, 200);
        window.ShowWindow(_window);
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


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
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
    }
}
