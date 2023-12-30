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
/*

#include <string>

class NSBitmapImageRep {
public:
    NSBitmapImageRep(unsigned char** bitmapDataPlanes, int pixelsWide, int pixelsHigh, int bitsPerSample, int samplesPerPixel, bool hasAlpha, bool isPlanar, std::string colorSpaceName, int bytesPerRow, int bitsPerPixel) {
        // Constructor implementation
    }
};

struct Pixel {
    // Define the Pixel struct if not already defined
};

NSBitmapImageRep* CreateImageWithHeightWidth(int width, int height, unsigned char* buffer, std::string colorName) {
    return new NSBitmapImageRep(&buffer, width, height, 8, 4, true, false, colorName, width * sizeof(Pixel), sizeof(Pixel) * 8);
}

*/