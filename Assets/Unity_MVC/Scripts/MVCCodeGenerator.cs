using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    public class MVCCodeGenerator
    {
        [MenuItem("Unity MVC/Open MVC Settings")]
        public static void OpenMVCSettings()
        {
            UnityMVCData data = GetMVCData();
            AssetDatabase.OpenAsset(data);
        }
        
        public static void CreateViewAndController(string name)
        {
            CreateScript(name, GetViewTemplate(), GetViewsPath(), "View");
            CreateScript(name, GetControllerTemplate(), GetControllersPath(), "Controller");
        }
        public static void CreateComponent(string name)
        {
            CreateScript(name, GetComponentTemplate(), GetComponentsPath(), "Component");
        }
        public static void CreateRepository(string name)
        {
            CreateScript(name, GetRepositoryTemplate(), GetRepositoriesPath(), "Repository");
        }
        public static void CreateService(string name)
        {
            CreateScript(name, GetServiceTemplate(), GetServicesPath(), "Service");
        }

        private static void CreateScript(string name, string template, string path, string templateType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");

            string templateStr = template;
            templateStr = templateStr.Replace($"{templateType}Template", $"{name}{templateType}");

            string directoryPath = path;
            string filePath = $"{directoryPath}/{name}{templateType}.cs";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                WriteFile(filePath, templateStr);
                Debug.Log($"{templateType} {filePath} created!");
            }
            AssetDatabase.Refresh();
        }

        private static void WriteFile(string path, string str)
        {
            StreamWriter file = File.CreateText(path);
            file.Write(str);
            file.Close();
        }

        private static UnityMVCData GetMVCData()
        {
            string[] asset = AssetDatabase.FindAssets("MVCData");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            UnityMVCData data = AssetDatabase.LoadAssetAtPath<UnityMVCData>(path);
            return data;
        }

        private static string GetControllerTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("ControllerTemplate");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetViewTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("ViewTemplate");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        private static string GetComponentTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("ComponentTemplate");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetRepositoryTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("RepositoryTemplate");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetServiceTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("ServiceTemplate");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }

        private static string GetControllersPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ControllersPath) || string.IsNullOrEmpty(data.Root))
            {
                return $"{assets}/Scripts/Controllers";
            }

            return $"{assets}/{data.ControllersPath}";
        }
        
        private static string GetViewsPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ViewsPath) || string.IsNullOrEmpty(data.Root))
            {
                return $"{assets}/Scripts/Views";
            }

            return $"{assets}/{data.ViewsPath}";
        }
        
        private static string GetComponentsPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ComponentsPath) || string.IsNullOrEmpty(data.Root))
            {
                return $"{assets}/Scripts/Components";
            }

            return $"{assets}/{data.ComponentsPath}";
        }
        
        private static string GetServicesPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ServicesPath) || string.IsNullOrEmpty(data.Root))
            {
                return $"{assets}/Scripts/Services";
            }

            return $"{assets}/{data.ServicesPath}";
        }
        
        private static string GetRepositoriesPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.RepositoriesPath) || string.IsNullOrEmpty(data.Root))
            {
                return $"{assets}/Scripts/Repositories";
            }

            return $"{assets}/{data.RepositoriesPath}";
        }
    }
}