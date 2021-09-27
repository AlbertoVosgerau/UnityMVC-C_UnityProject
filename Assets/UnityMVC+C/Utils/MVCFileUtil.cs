#if UNITY_EDITOR
using System.IO;

namespace UnityMVC.Editor
{
    public class MVCFileUtil
    {
        public static void ReplaceAndWriteFile(string path, string str)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            StreamWriter file = File.CreateText(path);
            file.Write(str);
            file.Close();
        }
        
        public static void WriteFile(string path, string str)
        {
            StreamWriter file = File.CreateText(path);
            file.Write(str);
            file.Close();
        }
    }
}
#endif