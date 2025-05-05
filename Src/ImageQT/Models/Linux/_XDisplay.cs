using System.Runtime.InteropServices;
using ImageQT.Models.Linux._private;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XDisplay
{
    public string? GetVendor =>
        Marshal.PtrToStringAnsi(Vendor);

    public string? GetLastRequest =>
        Marshal.PtrToStringAnsi(LastRequest);

    public string? GetBuffer =>
        Marshal.PtrToStringAnsi(Buffer);

    public string? GetDisplayName() =>
        Marshal.PtrToStringAnsi(DisplayName);

    public IntPtr ExtensionData; /* hook for Extension to hang data */
    public IntPtr FreeFunctions; /* internal free functions */
    public int FileDescriptor;         /* Network socket. */
    public int ConnectionChecker;         /* ugly thing used by _XEventsQueued */
    public int ProtocolMajorVersion;/* maj. version of server's X protocol */
    public int ProtocolMinorVersion;/* minor version of server's X protocol */
    public IntPtr Vendor;       /* vendor of the server hardware */
    public long ResourceBaseId;  /* resource ID base */
    public long ResourceIdMask;  /* resource ID mask bits */
    public long ResourceCurrentId;    /* allocator current ID */
    public int ResourceShiftBits; /* allocator shift to correct bits */
    //public delegate* unmanaged[Cdecl]<XDisplay*, long> ResourceAllocator;/* allocator function */
    public delegate* unmanaged[Cdecl]<IntPtr, long> ResourceAllocator;/* allocator function */
    public int ByteOrder;     /* screen byte order, LSBFirst, MSBFirst */
    public int BitmapUnit;    /* padding and data requirements */
    public int BitmapPadding;     /* padding requirements on bitmaps */
    public int BitmapBitOrder;   /* LeastSignificant or MostSignificant */
    public int NumberOfPixmapFormats;       /* number of pixmap formats in list */
    public IntPtr PixmapFormats;    /* pixmap format list */
    public int VersionNumber;        /* Xlib's X protocol version number. */
    public int Release;        /* Release of the server */
    public IntPtr Head;
    public IntPtr Tail; /* Input event queue. */
    public int QueueLength;       /* Length of input event queue */
    public ulong LastRequestRead; /* seq number of last event read */
    public ulong Request;  /* sequence number of last request. */
    public IntPtr LastRequest;     /* beginning of last request, or dummy */
    public IntPtr Buffer;       /* Output buffer starting address. */
    public IntPtr BufferPointer;       /* Output buffer index pointer. */
    public IntPtr BufferMaximum;       /* Output buffer maximum+1 address. */
    public uint MaxRequestSize; /* maximum number 32 bit words in request*/
    public IntPtr db;
    public delegate* unmanaged[Cdecl]<XDisplay*, int> SyncHandler; /* Synchronization handler */ //
    public IntPtr DisplayName; /* "host:display" string used on this connect*/
    public int DefaultScreen; /* default screen for operations */
    public int NumberOfScreens;       /* number of screens on this server*/
    public Screen* Screens;    /* pointer to list of screens */
    public ulong MotionBuffer;    /* size of motion buffer */
    public ulong Flags;      /* internal connection Flags */// volatile proprity
    public int MinimumKeyCode;    /* minimum defined keycode */
    public int MaximumKeyCode;    /* maximum defined keycode */
    public IntPtr Keysyms;    /* This server's Keysyms */
    public IntPtr ModifierKeyMap;   /* This server's modifier keymap */
    public int KeysymsPerKeycode;/* number of rows */
    public IntPtr XDefaults;    /* contents of defaults from server */
    public IntPtr ScratchBuffer;   /* place to hang scratch buffer */
    public ulong ScratchBufferLength;   /* length of scratch buffer */
    public int ExtensionNumber;     /* Extension number on this display */
    public _XExtension* ExtensionProcedures; /* extensions initialized on this display */
	/*
	 * the following can be fixed size, as the protocol defines how
	 * much address space is available.
	 * While this could be done using the Extension vector, there
	 * may be MANY events processed, so a search through the Extension
	 * list to find the right procedure for each event might be
	 * expensive if many extensions are being used.
	 */
    public EventVec EventVector;
    public WireVec WireVector;
    public long LockMeaning;       /* for XLookupString */
    public _XLockInfo* Lock;   /* multi-thread state, display Lock */
	public IntPtr AsyncHandlers; /* for internal async */
	public ulong BigRequestSize; /* max size of big requests */
    public _XLockPtrs* LockFns; /* pointers to threads functions */
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, int, void> IdListAllocator;  /* XID list allocator function */
    /* things above this line should not move, for binary compatibility */
    public IntPtr KeyBindings; /* for XLookupString */
	public ulong CursorFont;      /* for XCreateFontCursor */
    public IntPtr Atoms; /* for XInternAtom */
	public uint ModeSwitch;  /* keyboard group modifiers */
    public uint NumLock;  /* keyboard numlock modifiers */
    public IntPtr ContextDatabase; /* context database */
    public IntPtr ErrorVector;  /* vector for wire to error */
	/*
	 * Xcms information
	 */
	public XCms Cms;
	public IntPtr ImFilters;
	public IntPtr QFree; /* unallocated event queue elements */
	public ulong NextEventSerialNum; /* inserted into next queue elt */
    public _XExtension* Flushes; /* Flush hooks */
	public _XConnectionInfo* ImFdInfo; /* _XRegisterInternalConnection */
	public int ImFdLength;   /* number of ImFdInfo */
    public IntPtr ConnectionWatchers; /* XAddConnectionWatch */
	public int WatcherCount;  /* number of ConnectionWatchers */
    public IntPtr FileDes;   /* struct pollfd cache for _XWaitForReadable */
    public delegate* unmanaged[Cdecl]<XDisplay*, int> SavedSynChandler; /* user synchandler when Xlib usurps */
    public ulong ResourceMax;   /* allocator max ID */
    public int XCMiscOpcode;  /* major opcode for XC-MISC */
    public IntPtr XKBInfo; /* XKB info */
	public IntPtr TransportConnection; /* transport connection object */
	public _X11XCBPrivate* XCB; /* XCB glue private data */

	/* Generic event cookie handling */
	public uint NextCookie; /* next event cookie */
    /* vector for wire to generic event, index is (Extension - 128) */
    public GenericEventVec GenericEventVec;
    /* vector for event copy, index is (Extension - 128) */
    public GenericEventCopyVec GenericEventCopyVec;
	public IntPtr CookieJar;  /* cookie events returned but not claimed */

    public ulong LastRequestReadUpper32bit;
    public ulong RequestUpper32bit;

    public IntPtr ErrorThreads;
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr> XIOErrorExitHandler; /* WARNING, this type not in Xlib spec */
    public IntPtr exit_handler_data;
    public uint in_ifevent;
    public ulong ifevent_thread;

}
