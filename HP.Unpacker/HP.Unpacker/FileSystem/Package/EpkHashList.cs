using System;
using System.IO;
using System.Collections.Generic;

namespace HP.Unpacker
{
    class EpkHashList
    {
        private static String m_Path = Utils.iGetApplicationPath();
        private static String m_ProjectFile = @"\Projects\FileNames.list";
        private static String m_ProjectFilePath = m_Path + m_ProjectFile;

        private static Dictionary<UInt32, String> m_HashList = new Dictionary<UInt32, String>();

        public static void iLoadProject()
        {
            String m_Line = null;
            if (!File.Exists(m_ProjectFilePath))
            {
                Utils.iSetWarning("[WARNING]: Unable to load project file " + m_ProjectFile);
                return;
            }

            Int32 i = 0;
            m_HashList.Clear();

            StreamReader TProjectFile = new StreamReader(m_ProjectFilePath);
            while ((m_Line = TProjectFile.ReadLine()) != null)
            {
                UInt32 dwHash = EpkHash.iGetHash(m_Line.ToLower());

                if (m_HashList.ContainsKey(dwHash))
                {
                    String m_Collision = null;
                    m_HashList.TryGetValue(dwHash, out m_Collision);
                    Utils.iSetError("[COLLISION]: " + m_Collision + " <-> " + m_Line);
                }

                m_HashList.Add(dwHash, m_Line);
                i++;
            }

            TProjectFile.Close();
            Utils.iSetInfo("[INFO]: Project File Loaded: " + i.ToString());
            Console.WriteLine();
        }

        public static String iGetNameFromHashList(UInt32 dwHash)
        {
            String m_FileName = null;

            if (m_HashList.ContainsKey(dwHash))
            {
                m_HashList.TryGetValue(dwHash, out m_FileName);
            }
            else
            {
                m_FileName = @"__Unknown\" + dwHash.ToString("X8");
            }

            return m_FileName;
        }
    }
}
