using System;

namespace HP.Unpacker
{
    class EpkHeader
    {
        public UInt32 dwMagic { get; set; } //1A545352
        public UInt32 dwVersion { get; set; } // 0x20110224 & 0x20110425
        public UInt32 dwTableOffset { get; set; }
        public UInt32 dwBaseOffset { get; set; } // 64
        public UInt32 dwValidTableNum { get; set; }
        public UInt32 dwMaxTableNum { get; set; }
        public UInt32 dwPackageSize { get; set; }
        public Int32 dwTotalFiles { get; set; }
    }
}
