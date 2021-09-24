using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityMVC.Utils
{
    public class MVCAssetDatabaseUtil
    {
        public static string GetRelativeAssetPath(string name)
        {
            List<string> asset = AssetDatabase.FindAssets(name).ToList();
            string path = AssetDatabase.GUIDToAssetPath(asset.FirstOrDefault());
            return path;
        }
        
        public static string GetAbsoluteAssetPath(string name)
        {
            List<string> asset = AssetDatabase.FindAssets(name).ToList();
            string path = AssetDatabase.GUIDToAssetPath(asset.FirstOrDefault());
            path = path.Replace("Assets/", "");
            string absolutePath = $"{Application.dataPath}/{path}";
            return absolutePath;
        }
        
        public static string GetAssetGUID(string name)
        {
            List<string> asset = AssetDatabase.FindAssets(name).ToList();
            string GUID = asset.FirstOrDefault();
            return GUID;
        }

        public static string GetAssetWithExtensionGUID(string name, string extension)
        {
            List<string> asset = AssetDatabase.FindAssets(name).ToList();
            string GUID = asset.FirstOrDefault(x => x.Contains($".{extension}"));
            return GUID;
        }
        
        public static string GetAssetGUIDFromPath(string path)
        {
            string GUID = AssetDatabase.AssetPathToGUID(path);
            return GUID;
        }
    }
}