using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace HP.Unpacker
{
    class LZMA
    {
        public static Byte[] iDecompress(Byte[] lpSrcBuffer, Byte[] lpDstBuffer, Int32 dwDecompressedSize)
        {
            using (MemoryStream TDstMemoryStream = new MemoryStream())
            {
                using (MemoryStream TSrcMemoryStream = new MemoryStream(lpSrcBuffer))
                {
                    Decoder LZMADecoder = new Decoder();

                    Byte[] lpProperties = new Byte[5];
                    lpProperties[0] = 0x5D;
                    lpProperties[3] = 0x80;

                    LZMADecoder.SetDecoderProperties(lpProperties);
                    LZMADecoder.Code(TSrcMemoryStream, TDstMemoryStream, TSrcMemoryStream.Length, dwDecompressedSize, null);

                    lpDstBuffer = TDstMemoryStream.ToArray();
                }
            }
            return lpDstBuffer;
        }
    }
}
