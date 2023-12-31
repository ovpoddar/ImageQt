#include "AppKit/AppKit.h"

extern "C" {
     id CreateImageWithHeightWidth1(id arg1, SEL arg2, unsigned char* arg3,
        long arg4, long arg5, long arg6, long arg7,
        BOOL arg8, BOOL arg9, id arg10, long arg11,
        long arg12) {

        return ((id(*)(id, SEL, unsigned char**, long, long, long, long, BOOL, BOOL,
            id, long, long))objc_msgSend)(arg1, arg2, &arg3, arg4, arg5,
                arg6, arg7, arg8, arg9, arg10,
                arg11, arg12);
    }
}