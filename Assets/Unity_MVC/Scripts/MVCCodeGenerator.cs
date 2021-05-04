using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    enum ScriptType
    {
        View,
        Controller,
        Component,
        Repository,
        Service
    }
    public class MVCCodeGenerator
    {
        [MenuItem("Unity MVC/Open MVC Settings")]
        public static void OpenMVCSettings()
        {
            UnityMVCData data = GetMVCData();
            AssetDatabase.OpenAsset(data);
        }

        public static void CreateView(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.View), GetPath("Views"), ScriptType.View);
        }

        public static void CreateController(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Controller), GetPath("Controllers"), ScriptType.Controller);
        }
        
        public static void CreateViewAndController(string name)
        {
            CreateView(name);
            CreateController(name);
        }
        public static void CreateComponent(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Component), GetPath("Components"), ScriptType.Component);
        }
        public static void CreateRepository(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Repository), GetPath("Repositories"), ScriptType.Repository);
        }
        public static void CreateService(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Service), GetPath("Services"), ScriptType.Service);
        }

        private static void GenerateScript(string name, string template, string path, ScriptType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");

            string templateStr = template;
            string typeStr = type.ToString();
            templateStr = templateStr.Replace($"{typeStr}Template", $"{name}{typeStr}");
            if (type == ScriptType.View)
            {
                templateStr = templateStr.Replace($"ControllerTemplate", $"{name}Controller");
            }
            string directoryPath = path;
            string filePath = $"{directoryPath}/{name}{typeStr}.cs";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                WriteFile(filePath, templateStr);
                Debug.Log($"{typeStr} {filePath} created!");
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
        
        private static string GetTemplate(ScriptType type)
        {
            string[] asset = AssetDatabase.FindAssets($"{type.ToString()}Template");
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