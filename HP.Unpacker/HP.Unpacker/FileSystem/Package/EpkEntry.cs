using System;

namespace HP.Unpacker
{
    class EpkEntry
    {
        public UInt32 dwNameHash { get; set; } // Example -> data\effect\npc\huolong04.eft
        public UInt32 dwFolderHash { get; set; } // Example -> data\effect\npc
        public UInt32 dwOffset { get; set; }
        public Int32 dwCompressedSize { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public wEntryType wEntryType { get; set; }
        public wCompressionType wCompressionType { get; set; }
        public UInt16 wNameLength { get; set; }
        public UInt32 dwLowDateTime { get; set; }
        public UInt32 dwHighDateTime { get; set; }
        public Byte[] m_MD5 { get; set; }
    }
}
