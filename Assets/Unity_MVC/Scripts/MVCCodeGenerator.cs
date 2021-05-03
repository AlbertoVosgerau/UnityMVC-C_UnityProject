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
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");
            
            string controllerStr = GetControllerTemplate();
            controllerStr = controllerStr.Replace("ControllerTemplate", $"{name}Controller");
            controllerStr =controllerStr.Replace("ViewTemplate", $"{name}View");

            string viewStr = GetViewTemplate();
            viewStr = viewStr.Replace("ControllerTemplate", $"{name}Controller");
            viewStr = viewStr.Replace("ViewTemplate", $"{name}View");

            string controllersPath = GetControllersPath();
             string controllerFilePath = $"{controllersPath}/{name}Controller.cs";
             
             string viewsPath = GetViewsPath();
             string viewFilePath = $"{viewsPath}/{name}View.cs";

             if (!Directory.Exists(controllersPath))
             {
                 Directory.CreateDirectory(controllersPath);
             }

             if (!File.Exists(controllerFilePath))
             {
                 WriteFile(controllerFilePath, controllerStr);
                 Debug.Log($"Controller {controllerFilePath} created!");
             }
             
             if (!Directory.Exists(viewsPath))
             {
                 Directory.CreateDirectory(viewsPath);
             }
             
             if (!File.Exists(viewFilePath))
             {
                 WriteFile(viewFilePath, viewStr);
                 Debug.Log($"View {viewFilePath} created!");
             }
             AssetDatabase.Refresh();
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

        public static void CreateComponent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");
            string templateType = "Repository";
            
            string templateStr = GetComponentTemplate();
            templateStr = templateStr.Replace($"{templateType}Template", $"{name}{templateType}");

            string directoryPath = GetComponentsPath();
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

        public static void CreateRepository(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");
            string templateType = "Repository";
            
            string templateStr = GetRepositoryTemplate();
            templateStr = templateStr.Replace($"{templateType}Template", $"{name}{templateType}");

            string directoryPath = GetRepositoriesPath();
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

        public static void CreateService(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");
            string templateType = "Service";
            
            string templateStr = GetServiceTemplate();
            templateStr = templateStr.Replace($"{templateType}Template", $"{name}{templateType}");

            string directoryPath = GetServicesPath();
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
            Debug.Log($"Will write string: {str}");
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
            
            if (string.IsNullOrEmpty(data.ControllersPath))
            {
                return $"{assets}/Scripts/Controllers";
            }

            return $"{assets}/{data.ControllersPath}";
        }
        
        private static string GetViewsPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ViewsPath))
            {
                return $"{assets}/Scripts/Views";
            }

            return $"{assets}/{data.ViewsPath}";
        }
        
        private static string GetComponentsPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ComponentsPath))
            {
                return $"{assets}/Scripts/Components";
            }

            return $"{assets}/{data.ComponentsPath}";
        }
        
        private static string GetServicesPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.ServicesPath))
            {
                return $"{assets}/Scripts/Services";
            }

            return $"{assets}/{data.ServicesPath}";
        }
        
        private static string GetRepositoriesPath()
        {
            UnityMVCData data = GetMVCData();
            string assets = Application.dataPath;
            
            if (string.IsNullOrEmpty(data.RepositoriesPath))
            {
                return $"{assets}/Scripts/Repositories";
            }

            return $"{assets}/{data.RepositoriesPath}";
        }
    }
}