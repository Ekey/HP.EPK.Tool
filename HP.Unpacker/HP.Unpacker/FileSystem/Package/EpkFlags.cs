using System;

namespace HP.Unpacker
{
    public enum wEntryType : Byte
    {
        wFile = 0x1,
        wFolder = 0x3,
        wDeleted = 0x11,
    }

    public enum wCompressionType : Byte
    {
        Null = 0x0,
        Changeless = 0x1, // Uncompressed
        Lzo1X = 0x2, // Unused
        Lzo1X999 = 0x3, // Unused
        LZMA = 0x4,
        End = 0x5,
    }
}
