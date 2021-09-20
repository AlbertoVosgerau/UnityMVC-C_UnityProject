using System.IO;

namespace UnityMVC.Editor
{
    public class MVCFileUtil
    {
        public static void WriteFile(string path, string str)
        {
            StreamWriter file = File.CreateText(path);
            file.Write(str);
            file.Close();
        }
    }
}