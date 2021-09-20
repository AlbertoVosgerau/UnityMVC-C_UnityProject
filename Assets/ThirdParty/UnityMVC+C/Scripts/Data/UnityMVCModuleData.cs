using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityMVC.Editor
{
    [Serializable]
    public class UnityMVCModuleModel
    {
        public string moduleName;
        public string moduleNamespace;
        public string relativePath;

        public UnityMVCModuleModel(string moduleName, string moduleNamespace)
        {
            this.moduleName = moduleName;
            this.moduleNamespace = moduleNamespace;
        }
    }
    
    public class UnityMVCModuleData
    {
        public static string ModuleMetadataFileName => $"{_moduleMetadataFileName}.{_moduleMetadataExtension}";
        private static string _moduleMetadataFileName = "unitymvcdata";
        private static string _moduleMetadataExtension = "module";

        public static UnityMVCModuleModel GenerateModuleMetadata(string moduleAbsolutePath, string moduleName, string moduleNamespace)
        {
            string path = GetFullAbsolutePath(moduleAbsolutePath);

            if (File.Exists(path))
            {
                return ReadModuleDataFromFolder(path);
            }

            UnityMVCModuleModel data = new UnityMVCModuleModel(moduleName, moduleNamespace);
            string str = JsonUtility.ToJson(data);
            
            MVCFileUtil.WriteFile(path, str);
            return data;
        }

        public static UnityMVCModuleModel ReadModuleDataFromFolder(string moduleAbsolutePath)
        {
            string path = GetFullAbsolutePath(moduleAbsolutePath);
            if (!File.Exists(path))
            {
                return null;
            }

            return ReadMetadata(path);
        }
        
        public static UnityMVCModuleModel ReadModuleDataFromFile(string fileAbsolutePath)
        {
            if (!File.Exists(fileAbsolutePath))
            {
                return null;
            }

            return ReadMetadata(fileAbsolutePath);
        }

        private static UnityMVCModuleModel ReadMetadata(string path)
        {
            string str = File.ReadAllText(path);
            UnityMVCModuleModel data = JsonUtility.FromJson<UnityMVCModuleModel>(str);
            return data;
        }

        private static string GetFullAbsolutePath(string moduleAbsolutePath)
        {
            string absolutePath = $"{moduleAbsolutePath}/{ModuleMetadataFileName}";
            return absolutePath;
        }

        public static List<UnityMVCModuleModel> GetAllModules()
        {
            List<UnityMVCModuleModel> data = new List<UnityMVCModuleModel>();
            string[] assets = AssetDatabase.FindAssets(_moduleMetadataFileName);

            foreach (string asset in assets)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(asset);
                if (assetPath.Contains($"Assets/"))
                {
                    assetPath = assetPath.Remove(0, 7);
                }
                
                UnityMVCModuleModel dataFile = ReadModuleDataFromFile($"{Application.dataPath}/{assetPath}");
                dataFile.relativePath = assetPath;
                data.Add(dataFile);
            }
            return data;
        }
    }
}