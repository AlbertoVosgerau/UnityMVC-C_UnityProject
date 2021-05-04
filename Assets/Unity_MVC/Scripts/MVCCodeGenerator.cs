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
            GenerateScript(name, GetTemplate("View"), GetPath("Views"), "View");
            GenerateScript(name, GetTemplate("Controller"), GetPath("Controllers"), "Controller");
        }
        public static void CreateComponent(string name)
        {
            GenerateScript(name, GetTemplate("Component"), GetPath("Components"), "Component");
        }
        public static void CreateRepository(string name)
        {
            GenerateScript(name, GetTemplate("Repository"), GetPath("Repositories"), "Repository");
        }
        public static void CreateService(string name)
        {
            GenerateScript(name, GetTemplate("Service"), GetPath("Services"), "Service");
        }

        private static void GenerateScript(string name, string template, string path, string templateType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");

            string templateStr = template;
            templateStr = templateStr.Replace($"{templateType}Template", $"{name}{templateType}");
            if (templateType == "View")
            {
                templateStr = templateStr.Replace($"ControllerTemplate", $"{name}Controller");
            }
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
        
        private static string GetTemplate(string type)
        {
            string[] asset = AssetDatabase.FindAssets($"{type}Template");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        private static string GetPath(string type)
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ScriptsFolder))
            {
                return $"{assets}/Scripts/{type}";
            }

            return $"{assets}/{data.ScriptsFolder}/{type}";
        }
    }
}