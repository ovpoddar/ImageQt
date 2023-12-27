#include "AppKit/AppKit.h"

extern "C" {

    typedef struct {
        uint8_t red;
        uint8_t green;
        uint8_t blue;
        uint8_t alpha;
    } Pixel;

    NSBitmapImageRep* CreateImageWithHeightWidth(int width, int height, unsigned char* buffer, NSBitmapImageRep* obj, NSString* colorName) {
        return [obj initWithBitmapDataPlanes:&buffer
            pixelsWide:width 
            pixelsHigh:height
            bitsPerSample:8 
            samplesPerPixel:4 
            hasAlpha:YES 
            isPlanar:NO
            colorSpaceName:colorName
            bytesPerRow:width * sizeof(Pixel) 
            bitsPerPixel:sizeof(Pixel) * 8];
    }
}
