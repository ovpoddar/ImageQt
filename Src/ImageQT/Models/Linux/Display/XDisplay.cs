using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XDisplay
{
    public XExtData* ExtensionData; /* hook for extension to hang data */
    public FreeFuncs* FreeFunctions; /* internal free functions */
    public int Fd; /* Network socket. */
    public int Connection_Checker; /* ugly thing used by _XEventsQueued */
    public int ProtoMajorVersion; /* maj. version of server's X protocol */
    public int ProtoMinorVersion; /* minor version of server's X protocol */
    public char* Vendor; /* vendor of the server hardware */
    public long ResourceBaseId; /* resource ID base */
    public long ResourceIdMask; /* resource ID mask bits */
    public long ResourceCurrentId; /* allocator current ID */
    public int ResourceShiftBits; /* allocator shift to correct bits */
    public delegate* unmanaged[Cdecl]<XDisplay*, long> ResourceAllocator;
    public int ByteOrder; /* screen byte order, LSBFirst, MSBFirst */
    public int BitmapUnit; /* padding and data requirements */
    public int BitmapPadding; /* padding requirements on bitmaps */
    public int BitmapBitOrder; /* LeastSignificant or MostSignificant */
    public int NumberOfPixmapFormats; /* number of pixmap formats in list */
    public ScreenFormat* pixmap_format; /* pixmap format list */
    public int VersionNumber; /* Xlib's X protocol version number. */
    public int Release; /* Release of the server */
    public SQEvent* Head;
    public SQEvent* Tail; /* Input event queue. */
    public int QueueLength; /* Length of input event queue */
    public ulong LastRequestRead; /* seq number of last event read */
    public ulong Request; /* sequence number of last request. */
    public nint LastRequest; /* beginning of last request, or dummy */
    public nint Buffer; /* Output buffer starting address. */
    public nint BufferPointer; /* Output buffer index pointer. */
    public nint BufferMaximum; /* Output buffer maximum+1 address. */
    public uint MaxRequestSize; /* maximum number 32 bit words in request*/
    public _XrmHashBucketRec* db;
    public delegate* unmanaged[Cdecl]<XDisplay*, int> SyncHandler; /* Synchronization handler */ //
    public nint DisplayName; /* "host:display" string used on this connect*/
    public int DefaultScreen; /* default screen for operations */
    public int NumberOfScreens; /* number of screens on this server*/
    public Screen* Screens; /* pointer to list of screens */
    public ulong MotionBuffer; /* size of motion buffer */
    public ulong Flags; /* internal connection Flags */ // volatile proprity
    public int MinimumKeyCode; /* minimum defined keycode */
    public int MaximumKeyCode; /* maximum defined keycode */
    public ulong* KeySyms; /* This server's keysyms */
    public XModifierKeymap* ModifierKeyMap; /* This server's modifier keymap */
    public int KeySymsPerKeycode; /* number of rows */
    public nint XDefaults; /* contents of defaults from server */
    public nint ScratchBuffer; /* place to hang scratch buffer */
    public ulong ScratchBufferLength; /* length of scratch buffer */
    public int ExtensionNumber; /* Extension number on this display */

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
    public long LockMeaning; /* for XLookupString */
    public _XLockInfo* Lock; /* multi-thread state, display Lock */
    public _XInternalAsync* AsyncHandlers; /* for internal async */
    public ulong BigRequestSize; /* max size of big requests */
    public _XLockPtrs* LockFns; /* pointers to threads functions */

    public delegate* unmanaged[Cdecl]<nint, nint, int, void> IdListAllocator; /* XID list allocator function */

    /* things above this line should not move, for binary compatibility */
    public _XKeytrans* KeyBindings; /* for XLookupString */
    public ulong CursorFont; /* for XCreateFontCursor */
    public nint Atoms; /* for XInternAtom */ // todo: find a way to use this
    public uint ModeSwitch; /* keyboard group modifiers */
    public uint NumLock; /* keyboard numlock modifiers */
    public _XContextDB* ContextDb; /* context database */

    public nint ErrorVector; /* vector for wire to error */ // todo: need more info

    /*
     * Xcms information
     */
    public XCms Cms;
    public _XIMFilter* ImFilters;
    public SQEvent* QFree; /* unallocated event queue elements */
    public ulong NextEventSerialNum; /* inserted into next queue elt */
    public _XExtension* Flushes; /* Flush hooks */
    public _XConnectionInfo* ImFdInfo; /* _XRegisterInternalConnection */
    public int ImFdLength; /* number of ImFdInfo */
    public _XConnWatchInfo* ConnectionWatchers; /* XAddConnectionWatch */
    public int WatcherCount; /* number of ConnectionWatchers */
    public nint FileDes; /* struct pollfd cache for _XWaitForReadable */
    public delegate* unmanaged[Cdecl]<XDisplay*, int> SavedSynChandler; /* user synchandler when Xlib usurps */
    public ulong ResourceMax; /* allocator max ID */
    public int XCMiscOpcode; /* major opcode for XC-MISC */
    public _XkbInfoRec* XKBInfo; /* XKB info */
    public _XtransConnInfo* TransConn; /* transport connection object */
    public _X11XCBPrivate* XCB; /* XCB glue private data */

    /* Generic event cookie handling */
    public uint NextCookie; /* next event cookie */

    /* vector for wire to generic event, index is (extension - 128) */
    public GenericEventVec GenericEventVec;

    /* vector for event copy, index is (Extension - 128) */
    public GenericEventCopyVec GenericEventCopyVec;
    public nint CookieJar; /* cookie events returned but not claimed */
#if TARGET_32BIT
    public ulong LastRequestReadUpper32bit;
    public ulong RequestUpper32bit;
#endif

    public _XErrorThreadInfo* ErrorThreads;

    public delegate* unmanaged[Cdecl]<XDisplay*, nint, void> ExitHandler;
    public nint ExitHandlerData;
    public uint INIfEvent;
    public ulong IFEventThread;
}