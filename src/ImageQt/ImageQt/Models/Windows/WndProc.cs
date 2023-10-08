using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Windows;
internal class WndProc
{
    public delegate IntPtr WndProcDelegate(IntPtr hWnd,
        ProcessesMessage msg, 
        IntPtr wParam,
        IntPtr lParam);
}
