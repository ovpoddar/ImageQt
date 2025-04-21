using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Screen
{
    public IntPtr ext_data;             // XExtData*
    public IntPtr display;              // Display*
    public ulong root;                  // Window (XID, usually ulong)
    public int width;                   // screen width in pixels
    public int height;                  // screen height in pixels
    public int mwidth;                  // width in millimeters
    public int mheight;                 // height in millimeters
    public int ndepths;                 // number of supported depths
    public IntPtr depths;              // Depth* (list of depths)
    public int root_depth;             // bits per pixel
    public IntPtr root_visual;         // Visual*
    public ulong default_gc;           // GC (Graphics Context)
    public ulong cmap;                 // Colormap
    public ulong white_pixel;          // pixel value for white
    public ulong black_pixel;          // pixel value for black
    public int max_maps;               // max colormaps
    public int min_maps;               // min colormaps
    public int backing_store;          // enum: Never, WhenMapped, Always
    public int save_unders;            // Bool (0 or non-zero)
    public long root_input_mask;       // input event mask
}
