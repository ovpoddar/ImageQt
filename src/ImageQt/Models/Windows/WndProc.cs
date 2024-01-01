namespace ImageQt.Models.Windows;
internal class WndProc
{
    public delegate IntPtr WndProcDelegate(IntPtr hWnd,
        ProcessesMessage msg,
        IntPtr wParam,
        IntPtr lParam);
}
