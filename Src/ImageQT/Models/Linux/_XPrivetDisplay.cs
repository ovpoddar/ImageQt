using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XPrivateDisplay
{
    public IntPtr ext_data;               // XExtData*
    public IntPtr private1;              // struct _XPrivate*
    public int fd;
    public int private2;
    public int proto_major_version;
    public int proto_minor_version;
    public IntPtr vendor;                // char*
    public ulong private3;               // XID
    public ulong private4;               // XID
    public ulong private5;               // XID
    public int private6;
    public delegate* unmanaged[Cdecl]<IntPtr, int> resource_alloc;        // XID (*ResourceAllocator)(struct Display*)
    public int byte_order;
    public int bitmap_unit;
    public int bitmap_pad;
    public int bitmap_bit_order;
    public int nformats;
    public IntPtr pixmap_format;         // ScreenFormat*
    public int private8;
    public int release;
    public IntPtr private9;
    public IntPtr private10;
    public int qlen;
    public ulong last_request_read;
    public ulong request;
    public IntPtr private11;
    public IntPtr private12;
    public IntPtr private13;
    public IntPtr private14;
    public uint max_request_size;
    public IntPtr db;                    // struct _XrmHashBucketRec*
    public IntPtr private15;             // int (*private15)(struct Display*)
    public IntPtr display_name;          // char*
    public int default_screen;
    public int nscreens;
    public Screen* screens;               // Screen*
    public ulong motion_buffer;
    public ulong private16;
    public int min_keycode;
    public int max_keycode;
    public IntPtr private17;
    public IntPtr private18;
    public int private19;
    public IntPtr xdefaults;             // char*
    // Additional private fields exist in the native struct
}

