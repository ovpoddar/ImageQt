using ImageQT.DllInterop.Windows;
using ImageQT.Models.Windows;
using System.Runtime.InteropServices;
using static ImageQT.Models.Windows.WindowDelegate;

namespace ImageQT.Handlers.Window;
internal sealed class WindowManager : INativeWindowManager
{
    private string _hiddenClass;
    private WindowWrapper? _window;

    public nint CreateWindow(uint height, uint width)
    {
        _hiddenClass = Guid.NewGuid().ToString();
        var module = Kernel32.GetModuleHandleA(null);
        WndClassExW wc = new()
        {
            ClassSize = (uint)Marshal.SizeOf<WndClassExW>(),
            style = WindowStyle.DBLCLKS,
            lpFnWndProc = Marshal.GetFunctionPointerForDelegate<WndProcDelegate>(CustomWndProc),
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = module,
            hCursor = IntPtr.Zero,
            hIcon = IntPtr.Zero,
            hBrBackground = IntPtr.Zero,
            lpszMenuName = "",
            lpszClassName = _hiddenClass,
            hIconSm = IntPtr.Zero,
        };
        var atom = User32.RegisterClassExW(ref wc);
        if (atom == 0)
            throw new Exception("Could not Register Window");

        _window = User32.CreateWindowExW(
            0,
            wc.lpszClassName,
            "",
            13565952,
            0,
            0,
            (int)width,
            (int)height,
            IntPtr.Zero,
            IntPtr.Zero,
            module,
            IntPtr.Zero);
        return default;

    }

    public void Dispose()
    {
        if (_window is null) 
            return;

        if (!_window.IsInvalid)
            _window.Dispose();

        var indtance = Kernel32.GetModuleHandleA(null);
        User32.UnregisterClassW(_hiddenClass, indtance);

    }

    public Task Show()
    {
        User32.ShowWindow(_window, 5);
        var message = new LpMsg();

        while (User32.GetMessage(out message, 0, 0, 0) != 0)
        {
            User32.DispatchMessage(ref message);
        }
        return Task.CompletedTask;
    }

    IntPtr CustomWndProc(IntPtr hWnd, ProcessesMessage msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            case ProcessesMessage.WM_DESTROY:
                User32.PostQuitMessage(0);
                return 0;
        }

        return User32.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }
}
