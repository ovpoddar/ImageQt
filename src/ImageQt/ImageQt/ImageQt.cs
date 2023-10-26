using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ImageQt;

public class ImageQt : IDisposable
{
    private bool _disposed;
    private IntPtr _window;
    private IntPtr _imagePixeldata;
    private BitmapInfo _imageData;
    private Windows _display;


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
            _display.CleanUpResources(ref _window);

            if (_imagePixeldata != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_imagePixeldata);
                _imagePixeldata = IntPtr.Zero;
            }
        }
    }
}
