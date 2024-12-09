#if DEBUG || Windows
using ImageQT.DllInterop.Windows;
using ImageQT.Models.Windows;
using System.Runtime.InteropServices;
using static ImageQT.Models.Windows.WindowDelegate;

namespace ImageQT.Handlers.Window;
internal sealed class WindowManager : INativeWindowManager
{
    private readonly string _hiddenClass;
    private WindowWrapper? _window;

    private IntPtr _imagePixelData;
    private BitmapInfo _imageData;

    private readonly WndProcDelegate _wndProcDelegate;

    public WindowManager()
    {
        _hiddenClass = Guid.NewGuid().ToString();
        _wndProcDelegate = CustomWndProc;
    }

    public void CreateWindow(uint height, uint width)
    {
        var module = Kernel32.GetModuleHandleA(null);
        WndClassExW wc = new()
        {
            ClassSize = (uint)Marshal.SizeOf<WndClassExW>(),
            style = WindowStyle.DBLCLKS,
            lpFnWndProc = Marshal.GetFunctionPointerForDelegate(_wndProcDelegate),
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = module,
            hCursor = IntPtr.Zero,
            hIcon = IntPtr.Zero,
            hBrBackground = IntPtr.Zero,
            lpszMenuName = string.Empty,
            lpszClassName = _hiddenClass,
            hIconSm = IntPtr.Zero,
        };
        var atom = User32.RegisterClassExW(ref wc);
        if (atom == 0)
            throw new Exception("Could not Register Window");

        _window = User32.CreateWindowExW(
            0,
            wc.lpszClassName,
            string.Empty,
            13565952,
            0,
            0,
            (int)width,
            (int)height,
            IntPtr.Zero,
            IntPtr.Zero,
            module,
            IntPtr.Zero);
    }

    public void Dispose()
    {
        if (_window is null)
            return;

        if (!_window.IsInvalid)
            _window.Dispose();

        var indtance = Kernel32.GetModuleHandleA(null);
        User32.UnregisterClassW(_hiddenClass, indtance);
        GC.SuppressFinalize(this);
    }

    public void SetUpImage(Image image)
    {
        _imageData = new()
        {
            biSize = Marshal.SizeOf<BitmapInfo>(),
            biWidth = image.Width,
            biHeight = image.Height * -1,
            biPlanes = 1,
            biBitCount = image.BitCount,
            biCompression = 0
        };
        _imagePixelData = image.Id.AddrOfPinnedObject();
    }

    public Task Show(DateTime? closeTime = null)
    {
        if (_window is null)
            return Task.CompletedTask;

        User32.ShowWindow(_window, 5);

        while (User32.GetMessage(out var message, 0, 0, 0) != 0)
        {
            if (_imagePixelData == IntPtr.Zero
                || closeTime != null && closeTime.Value < DateTime.Now)
            {
                User32.PostQuitMessage(0);
                break;
            }

            IntPtr currentDeviceContext = User32.GetDC(_window);
            GDI32.StretchDIBits(currentDeviceContext,
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
            User32.ReleaseDC(_window, currentDeviceContext);

            User32.DispatchMessage(ref message);
        }
        return Task.CompletedTask;
    }

    IntPtr CustomWndProc(IntPtr hWnd, ProcessesMessage msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case ProcessesMessage.Destroy:
                User32.PostQuitMessage(0);
                return 0;
        }
        return User32.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }
}
#endif