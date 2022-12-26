using System;
using System.IO;

namespace HP.Unpacker
{
    class Program
    {
        //hp.70yx.com
        private static String m_Title = "hp.70yx EPK Unpacker";

        static void Main(String[] args)
        {
            Console.Title = m_Title;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("HP PEK Unpacker");
            Console.WriteLine("(c) 2022 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    HP.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of EPK file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    HP.Unpacker E:\\Games\\HP\\Data\\data0.epk D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_EpkFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);

            if (!File.Exists(m_EpkFile))
            {
                Utils.iSetError("[ERROR]: Input EPK file -> " + m_EpkFile + " <- does not exist");
                return;
            }

            EpkUnpack.iDoIt(m_EpkFile, m_Output);
        }
    }
}
