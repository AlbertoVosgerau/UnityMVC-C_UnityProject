using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityMVC;
using UnityMVC.Component;
using UnityMVC.Controller;
using UnityMVC.Model;
using UnityMVC.View;

public class MVCToolsMenu : EditorWindow
{
    private class PartialUpdateData
    {
        public string name;
        public string path;
        public ScriptType type;
        public string baseType;
        public string view;
    }

    [MenuItem("Unity MVC/Tools/Update Partials", priority = 200)]
    private static void UpdatePartials()
    {
        var controllers = GetPaths(ScriptType.Controller, typeof(Controller));
        var views = GetPaths(ScriptType.View, typeof(View));
        var components = GetPaths(ScriptType.MVCComponent, typeof(MVCComponent));

        foreach (var item in controllers)
        {
            MVCCodeGenerator.UpdatePartial(item.type, item.name, item.baseType, item.path, item.view);
        }
        foreach (var item in views)
        {
            MVCCodeGenerator.UpdatePartial(item.type, item.name, item.baseType, item.path, item.view);
        }
        foreach (var item in components)
        {
            MVCCodeGenerator.UpdatePartial(item.type, item.name, item.baseType, item.path, item.view);
        }
    }

    private static List<PartialUpdateData> GetPaths(ScriptType scriptType, Type objectType)
    {
        List<PartialUpdateData> pathList = new List<PartialUpdateData>();
        var types = Assembly.GetAssembly(objectType).GetTypes().Where(myType => myType.IsClass && myType.IsSubclassOf(objectType) && !myType.Name.Contains("Template"));
        foreach (Type type in types)
        {
            string str = type.Name.Replace(scriptType.ToString(), "");
            string[] assets = AssetDatabase.FindAssets(type.Name);
            foreach (var item in assets)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.Contains("Partial.cs"))
                {
                    string view = null;
                    if (path.Contains("MVCComponent"))
                    {
                        view = GetPartialViewReference(path);
                        Debug.Log($"View:{view}");
                    }
                    
                    var plist = path.Split('/').ToList();
                    plist.Remove(plist.Last());
                    path = string.Join("/", plist);
                    pathList.Add(new PartialUpdateData { name = type.Name.Replace(scriptType.ToString(), ""), type = scriptType, path = path, baseType = type.BaseType.Name , view = view});
                }
            }
        }
        return pathList;
    }

    private static string GetPartialViewReference(string path)
    {
        string str = File.ReadAllText(path);
        
        List<string> splitStr = str.Split(new[]{ Environment.NewLine }, StringSplitOptions.None).ToList();
        string line = splitStr.FirstOrDefault(x => x.Contains("return typeof("));
        line = line.Replace("return typeof(", "");
        str = line.Replace(");", "");
        str = str.Replace(" ", "");
        return str;
    }
}
