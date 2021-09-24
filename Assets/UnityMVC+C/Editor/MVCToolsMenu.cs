using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityMVC.CodeGenerator;
using UnityMVC.Component;
using UnityMVC.Controller;
using UnityMVC.Editor;
using UnityMVC.View;

public class MVCToolsMenu : EditorWindow
{
    public class PartialUpdateData
    {
        public string nameSpace;
        public string name;
        public string path;
        public ScriptType type;
        public string baseType;
        public string view;
    }

    public static List<PartialUpdateData> GetPaths(ScriptType scriptType, Type objectType)
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
                    string nameSpace = GetNamespace(path);
                    string view = null;
                    if (path.Contains("MVCComponent"))
                    {
                        view = GetPartialViewReference(path);
                    }

                    var plist = path.Split('/').ToList();
                    plist.Remove(plist.Last());
                    path = string.Join("/", plist);
                    
                    pathList.Add(new PartialUpdateData {nameSpace = nameSpace, name = type.Name.Replace(scriptType.ToString(), ""), type = scriptType, path = path, baseType = type.BaseType.Name , view = view});
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

    private static string GetNamespace(string path)
    {
        string str = File.ReadAllText(path);
        List<string> splitStr = str.Split(new[]{ Environment.NewLine }, StringSplitOptions.None).ToList();
        string line = splitStr.FirstOrDefault(x => x.Contains("namespace"));
        if (line != null)
        {
            string nameSpace = line.Replace("namespace ", "");
            return nameSpace;
        }
        return null;
    }
}
