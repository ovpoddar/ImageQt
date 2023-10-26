using ImageQt.CallerPInvoke.Windows;
using ImageQt.Models.Windows;
using System.Runtime.InteropServices;

namespace ImageQt.Handler.Windows;

internal class Window : IWindow
{
    private WndProc.WndProcDelegate _wndProcDelegate;

    public nint DeclareWindow(string windowTitle, uint height, uint width)
    {
        nint instance = Kernel.GetModuleHandle(null);

        _wndProcDelegate = CustomWndProc;
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
            "Some Name",
            13565952,
            -2147483648,
            -2147483648,
            (int)height,
            (int)width,
            nint.Zero,
            nint.Zero,
            instance,
            nint.Zero);
    }

    public void ShowWindow(nint window)
    {
        if (window == nint.Zero)
            throw new Exception("Could not Initilized.");
        Win.ShowWindow(window, 5);
    }

    public void ProcessEvent(nint window)
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
                //case ProcessesMessage.WM_PAINT:
                //    LoadImageFromData(hWnd);
                //    return 0;
        }


        return Win.DefWindowProcW(hWnd, (uint)msg, wParam, lParam);
    }

    public void CleanUpResources(GCHandle window)
    {
        if (!window.IsAllocated)
            return;

        var ptr = window.AddrOfPinnedObject();
        Win.DestroyWindow(ptr);
        window.Free();
    }
}