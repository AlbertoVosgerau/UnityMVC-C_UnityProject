using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    #if UNITY_EDITOR
    enum ScriptType
    {
        View,
        Controller,
        MVCComponent,
        Container,
        Loader,
        Solver
    }
    public class MVCCodeGenerator
    {
        public static void CreateView( string name)
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
            GenerateScript(name, GetTemplate(ScriptType.MVCComponent), GetPath("Components"), ScriptType.MVCComponent);
        }
        public static void CreateContainer(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Container), GetPath("Containers"), ScriptType.Container);
        }
        public static void CreateLoader(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Loader), GetPath("Loaders"), ScriptType.Loader);
        }
        
        public static void CreateSolver(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.Solver), GetPath("Solvers"), ScriptType.Solver);
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
            if (type == ScriptType.Controller)
            {
                templateStr = templateStr.Replace($"ViewTemplate", $"{name}View");
            }
            if (type == ScriptType.MVCComponent)
            {
                templateStr = templateStr.Replace($"ComponentTemplateEvents", $"{name}ComponentEvents");
            }
            if (type == ScriptType.MVCComponent)
            {
                templateStr = templateStr.Replace($"ComponentTemplateInfo", $"{name}ComponentInfo");
            }
            if (type == ScriptType.Container)
            {
                templateStr = templateStr.Replace($"LoaderTemplate", $"{name}Loader");
            }
            if (type == ScriptType.Loader)
            {
                templateStr = templateStr.Replace($"SolverTemplate", $"{name}Solver");
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

        public static UnityMVCData GetMVCData()
        {
            string[] asset = AssetDatabase.FindAssets("MVCData");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            UnityMVCData data = AssetDatabase.LoadAssetAtPath<UnityMVCData>(path);
            return data;
        }
        
        private static string GetTemplate(ScriptType type)
        {
            string[] assets = AssetDatabase.FindAssets($"{type.ToString()}Template");
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        private static string GetPath(string type)
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.scriptsFolder))
            {
                return $"{assets}/Scripts/{type}";
            }
            return $"{assets}/{data.scriptsFolder}/{type}";
        }
    }
    #endif
}