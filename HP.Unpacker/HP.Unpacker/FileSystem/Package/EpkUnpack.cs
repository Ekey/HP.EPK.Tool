using System;
using System.IO;
using System.Collections.Generic;

namespace HP.Unpacker
{
    class EpkUnpack
    {
        private static List<EpkEntry> m_EntryTable = new List<EpkEntry>();

        public static void iDoIt(String m_Archive, String m_DstFolder)
        {
            EpkHashList.iLoadProject();

            using (FileStream TEpkStream = File.OpenRead(m_Archive))
            {
                var m_Header = new EpkHeader();

                m_Header.dwMagic = TEpkStream.ReadUInt32();
                m_Header.dwVersion = TEpkStream.ReadUInt32();
                m_Header.dwTableOffset = TEpkStream.ReadUInt32();
                m_Header.dwBaseOffset = TEpkStream.ReadUInt32();
                m_Header.dwValidTableNum = TEpkStream.ReadUInt32();
                m_Header.dwMaxTableNum = TEpkStream.ReadUInt32();
                m_Header.dwPackageSize = TEpkStream.ReadUInt32();
                m_Header.dwTotalFiles = TEpkStream.ReadInt32();

                if (m_Header.dwMagic != 0x1A545352)
                {
                    throw new Exception("[ERROR]: Invalid magic of EPK archive file!");
                }

                if (m_Header.dwVersion != 0x20110224 && m_Header.dwVersion != 0x20110425)
                {
                    throw new Exception("[ERROR]: Invalid EPK archive version!");
                }

                TEpkStream.Seek(m_Header.dwTableOffset, SeekOrigin.Begin);

                m_EntryTable.Clear();
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    var m_Entry = new EpkEntry();

                    m_Entry.dwNameHash = TEpkStream.ReadUInt32();
                    m_Entry.dwFolderHash = TEpkStream.ReadUInt32();
                    m_Entry.dwOffset = TEpkStream.ReadUInt32();
                    m_Entry.dwCompressedSize = TEpkStream.ReadInt32();
                    m_Entry.dwDecompressedSize = TEpkStream.ReadInt32();
                    m_Entry.wEntryType = (wEntryType)TEpkStream.ReadByte();
                    m_Entry.wCompressionType = (wCompressionType)TEpkStream.ReadByte();
                    m_Entry.wNameLength = TEpkStream.ReadUInt16();
                    m_Entry.dwLowDateTime = TEpkStream.ReadUInt32();
                    m_Entry.dwHighDateTime = TEpkStream.ReadUInt32();
                    m_Entry.m_MD5 = TEpkStream.ReadBytes(16);

                    m_EntryTable.Add(m_Entry);
                }

                foreach (var m_Entry in m_EntryTable)
                {
                    if (m_Entry.wEntryType != wEntryType.wFolder
                        && m_Entry.wEntryType != wEntryType.wDeleted

                        //Dynamic LZMA data (data0.epk)
                        && m_Entry.dwNameHash != 0x0623D8A4
                        && m_Entry.dwNameHash != 0x12C1CBCB
                        && m_Entry.dwNameHash != 0x17440A52
                        )
                    {
                        String m_FileName = EpkHashList.iGetNameFromHashList(m_Entry.dwNameHash);
                        String m_FullPath = m_DstFolder + m_FileName;

                        Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                        Utils.iCreateDirectory(m_FullPath);

                        TEpkStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);
                        var lpSrcBuffer = TEpkStream.ReadBytes(m_Entry.dwCompressedSize);

                        if (m_Entry.dwCompressedSize == m_Entry.dwDecompressedSize)
                        {
                            File.WriteAllBytes(m_FullPath, lpSrcBuffer);

                        }
                        else
                        {
                            Byte[] lpDstBuffer = new Byte[m_Entry.dwDecompressedSize];
                            lpDstBuffer = LZMA.iDecompress(lpSrcBuffer, lpDstBuffer, m_Entry.dwDecompressedSize);

                            UInt32 dwXmlMagic = BitConverter.ToUInt32(lpDstBuffer, 0);

                            File.WriteAllBytes(m_FullPath, lpDstBuffer);
                        }
                    }
                }

                TEpkStream.Dispose();
            }
        }
    }
}
