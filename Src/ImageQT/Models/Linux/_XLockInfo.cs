using ImageQT.Models.Linux.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XLockInfo
{
    public IntPtr Mutex;     /* mutex for critical sections */
    public int ReplyBytesLeft;   /* nbytes of the reply still to read */
    public bool ReplyWasRead;    /* _XReadEvents read a reply for _XReply */
    public _XCVList* ReplyAwaiters; /* list of CVs for _XReply */
    public IntPtr ReplyAwaitersTail;
    public _XCVList* EventAwaiters; /* list of CVs for _XReadEvents */
    public IntPtr EventAwaitersTail;
    public bool reply_first;   /* who may read, reply queue or event queue */
    /* for XLockDisplay */
    public int LockingLevel;  /* how many times into XLockDisplay we are */
    public ulong LockingThread; /* thread that did XLockDisplay */
    public IntPtr Cv;    /* wait if another thread has XLockDisplay */
    public ulong ReadingThread; /* cache */
    public ulong ConnectionThread; /* thread in XProcessInternalConnection */
    public IntPtr Writers;   /* wait for writable */
    public int NumberFreeCVls;
    public _XCVList* FreeCVls;
    /* used only in XlibInt.c */
    public delegate* unmanaged[Cdecl]<XDisplay*, _XCVList**, _XCVList***, void> PopReader;
    public delegate* unmanaged[Cdecl]<XDisplay*, _XCVList***, _XCVList*> PushReader;
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int, void> ConditionWait;
    public delegate* unmanaged[Cdecl]<XDisplay*, bool, IntPtr, int, void> InternalLockDisplay;
	/* used in XlibInt.c and locking.c */
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, int, void> ConditionSignal;
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, int, void> ConditionBroadcast;
    /* used in XlibInt.c and XLockDis.c */
    public delegate* unmanaged[Cdecl]<XDisplay*, void> LockWait;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> UserLockDisplay;
    public delegate* unmanaged[Cdecl]<XDisplay*, void> UserUnlockDisplay;
    public delegate* unmanaged[Cdecl]<XDisplay*, _XCVList*> CreateCvl;
    
}
