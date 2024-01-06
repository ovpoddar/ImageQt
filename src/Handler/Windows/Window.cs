using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQt.Handler.Windows;

internal class Window : IWindow
{
    private readonly WndProc.WndProcDelegate _wndProcDelegate;

    private IntPtr _imagePixelData;
    private BitmapInfo _imageData;

    public Window() =>
        _wndProcDelegate = CustomWndProc;

    public IntPtr DeclareWindow(string windowTitle, uint height, uint width)
    {
        IntPtr instance = Kernel.GetModuleHandle(null);

        WindowStruct wc = new()
        {
            hInstance = instance,
            lpszClassName = windowTitle,
            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_wndProcDelegate)
        };

        Win.RegisterClassW(ref wc);
        return Win.CreateWindowExW(
            0,
            windowTitle,
            windowTitle,
            13565952,
            -2147483648,
            -2147483648,
            (int)height,
            (int)width,
            IntPtr.Zero,
            IntPtr.Zero,
            instance,
            IntPtr.Zero);
    }

    public void ShowWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
            throw new Exception("Could not Initilized.");
        Win.ShowWindow(window, 5);
    }

    public void ProcessEvent(IntPtr window)
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

    private nint CustomWndProc(nint hWnd, ProcessesMessage msg, nint wParam, nint lParam)
    {
        switch (msg)
        {
            case ProcessesMessage.WM_DESTROY:
                Win.PostQuitMessage(0);
                return 0;
            case ProcessesMessage.WM_PAINT:
                DrawImageFromPointer(hWnd);
                return 0;
        }


        return Win.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }

    public void CleanUpResources(ref IntPtr window)
    {
        if (window != IntPtr.Zero)
        {
            Win.DestroyWindow(window);
            window = IntPtr.Zero;
        }

        if (_imagePixelData != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_imagePixelData);
            _imagePixelData = IntPtr.Zero;
        }
    }

    private void DrawImageFromPointer(IntPtr hWnd)
    {
        if (_imagePixelData == IntPtr.Zero) return;
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
            _imagePixelData,
            ref _imageData,
            ColorUsage.DIB_RGB_COLORS,
            DropType.SrcCopy);
        Win.ReleaseDC(hWnd, currentDeviceContext);
    }

    public void LoadBitMap(int width, int height, ref IntPtr ImageData, IntPtr _)
    {
        _imageData = new()
        {
            biSize = Marshal.SizeOf<BitmapInfo>(),
            biWidth = width,
            biHeight = -height,
            biPlanes = 1,
            biBitCount = 32,
            biCompression = 0
        };
        _imagePixelData = ImageData;
    }
}