using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    #if UNITY_EDITOR
    public enum ScriptType
    {
        MVCApplication,
        View,
        Controller,
        MVCComponent,
        Container,
        Loader,
        Solver
    }
    public class MVCCodeGenerator
    {
        public static void CreateApplication(string name)
        {
            GenerateScript(name, GetTemplate(ScriptType.MVCApplication, true), GetPath("Application"), ScriptType.MVCApplication);
        }
        
        public static void CreateView(string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.View, removeComments), GetPath("Views"), ScriptType.View, inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null);
            GenerateScript(name, GetTemplate(ScriptType.View, removeComments, true), GetPath("Views"), ScriptType.View, null, true, inheritsFrom, inheritsFrom != null);
        }

        public static void CreateController(string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.Controller, removeComments), GetPath("Controllers"), ScriptType.Controller, virtualToOverride: inheritsFrom != null);
            GenerateScript(name, GetTemplate(ScriptType.Controller, removeComments,true), GetPath("Controllers"), ScriptType.Controller, null, true, inheritsFrom, inheritsFrom != null);
        }
        
        public static void CreateViewAndController(string name, bool removeComments, string controllerInheritsFrom = null, string viewInheritsFrom = null)
        {
            CreateController(name, removeComments, controllerInheritsFrom);
            CreateView(name, removeComments, viewInheritsFrom);
        }
        public static void CreateComponent(string name, bool removeComments, string view, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.MVCComponent, removeComments), GetPath("Components"), ScriptType.MVCComponent, view, inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null);
            GenerateScript(name, GetTemplate(ScriptType.MVCComponent,removeComments,  true), GetPath("Components", true), ScriptType.MVCComponent, view,true, inheritsFrom, inheritsFrom != null);
        }
        public static void CreateContainer(string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.Container, removeComments), GetPath("Containers"), ScriptType.Container, virtualToOverride: inheritsFrom != null);
            GenerateScript(name, GetTemplate(ScriptType.Container, removeComments, true), GetPath("Containers"), ScriptType.Container, null,true, inheritsFrom, inheritsFrom != null);
        }
        public static void CreateLoader(string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.Loader, removeComments), GetPath("Loaders"), ScriptType.Loader, virtualToOverride: inheritsFrom != null);
            GenerateScript(name, GetTemplate(ScriptType.Loader, removeComments, true), GetPath("Loaders"), ScriptType.Loader, null, true, inheritsFrom, inheritsFrom != null);
        }
        
        public static void CreateSolver(string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(name, GetTemplate(ScriptType.Solver, removeComments), GetPath("Solvers"), ScriptType.Solver, virtualToOverride: inheritsFrom != null);
        }

        public static void UpdatePartial(ScriptType type, string name, string baseType, string path, string view)
        {
            string template = GetTemplate(type, true, true);
            GenerateScript(name, template, path, type, isPartial: true, inheritsFrom: baseType, overrideFile: true, view: view);
        }

        private static void GenerateScript(string name, string template, string path, ScriptType type, string view = null, bool isPartial = false, string inheritsFrom = null, bool virtualToOverride = false, bool overrideFile = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");

            string templateStr = template;
            string typeStr = type.ToString();
            templateStr = templateStr.Replace($"{typeStr}Template", $"{name}{typeStr}");

            if (inheritsFrom != null)
            {
                if (isPartial)
                {
                    ApplyInheritanceToPartial(ref templateStr, type, inheritsFrom);
                }
                else
                {
                    //ApplyInheritanceToClass(ref templateStr, type, inheritsFrom);
                    ApplyInheritanceToModel(ref templateStr, name,type, inheritsFrom);
                }
            }

            if (virtualToOverride)
            {
                ChangeVirtualToOverride(ref templateStr);
            }
            else
            {
                RemoveNewKeywork(ref templateStr);
            }

            if (type == ScriptType.View)
            {
                templateStr = templateStr.Replace($"ControllerTemplate", $"{name}Controller");
                templateStr = templateStr.Replace($"ViewTemplateModel", $"{name}ViewModel");
                
            }
            if (type == ScriptType.Controller)
            {
                templateStr = templateStr.Replace($"ViewTemplate", $"{name}View");
            }
            if (type == ScriptType.MVCComponent)
            {
                templateStr = templateStr.Replace($"MVCComponentTemplateModel", $"{name}ComponentModel");
                
                if (view != null)
                {
                    templateStr = templateStr.Replace("ViewTemplate", view);
                    string simpleViewName = view.Replace("View", "");
                    templateStr = templateStr.Replace($"ControllerTemplate", $"{simpleViewName}Controller");
                }
               
            }
            if (type == ScriptType.Container)
            {
                templateStr = templateStr.Replace($"LoaderTemplate", $"{name}Loader");
            }
            if (type == ScriptType.Loader)
            {
                templateStr = templateStr.Replace($"SolverTemplate", $"{name}Solver");
            }

            templateStr = templateStr.Replace($"ControllerTemplateEvents", $"{name}ControllerEvents");
            
            string directoryPath = path;
            string filePath = isPartial? $"{directoryPath}/{name}{typeStr}Partial.cs" : $"{directoryPath}/{name}{typeStr}.cs";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath) || overrideFile)
            {
                WriteFile(filePath, templateStr);
                Debug.Log($"{typeStr} {filePath} created!");
            }
            AssetDatabase.Refresh();
        }

        private static void ApplyInheritanceToPartial(ref string from, ScriptType type, string to)
        {
            from = from.Replace($": {type}", $": {to}");
        }
        
        private static void ApplyInheritanceToClass(ref string from, ScriptType type, string to)
        {
            from = from.Replace($"{type}Model", $"{type}Model : {to}Model");
        }
        
        private static void ApplyInheritanceToModel(ref string from, string name, ScriptType type, string to)
        {
            from = from.Replace($"MVCModel", $"{to}Model");
        }

        private static void ChangeVirtualToOverride(ref string str)
        {
            str = str.Replace($"virtual", $"override");
            str = str.Replace("/*new*/", "new");
        }

        private static void RemoveNewKeywork(ref string str)
        {
            str = str.Replace(" /*new*/", "");
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

        public static string GetMVCDataPath()
        {
            string[] asset = AssetDatabase.FindAssets("MVCData");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            List<string> directories = path.Split(new[] { "/", "\\" }, StringSplitOptions.None).ToList();
            directories.RemoveAt(directories.Count-1);
            path = String.Join("/", directories);
            return path;
        }
        
        private static string GetTemplate(ScriptType type,bool removeComments, bool isPartial = false)
        {
            string templateName = isPartial? $"{type.ToString()}TemplatePartial" : $"{type.ToString()}Template";
            string[] assets = AssetDatabase.FindAssets(templateName);
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);    

            if (removeComments)
            {
                str = StringWithoutComments(str);
            }
            return str;
        }

        private static String StringWithoutComments(string str)
        {
            string[] lines = str.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var removedCommentsStr = lines.Where(x => !x.Contains("//") || x.Contains("////")).ToArray();
            str = String.Join("\n", removedCommentsStr);
            str = str.Replace("////", "//");
            return str;
        }
        private static string GetPath(string type, bool isPartial = false)
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;

            if (string.IsNullOrEmpty(data.editorData.scriptsFolder))
            {
                return $"{assets}/Scripts/{type}";
            }
            return $"{assets}/{data.editorData.scriptsFolder}/{type}";
        }
    }
    #endif
}