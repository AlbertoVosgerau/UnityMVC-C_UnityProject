using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

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
        
        [MenuItem("Unity MVC/CreateController")]
        public static void CreateViewAndController()
        {
            string name = "Dummy";
            
            string controllerStr = GetControllerTemplate();
            controllerStr = controllerStr.Replace("UnityController", $"{name}Controller");
            controllerStr =controllerStr.Replace("UnityView", $"{name}View");
            controllerStr = controllerStr.Replace($"class {name}Controller", $"class {name}Controller : UnityController");
            controllerStr = controllerStr.Replace("virtual", "override");
            
            string viewStr = GetViewTemplate();
            viewStr = viewStr.Replace("UnityController", $"{name}Controller");
            viewStr = viewStr.Replace("UnityView", $"{name}View");
            viewStr = viewStr.Replace($"MonoBehaviour", $"UnityView");
            viewStr = viewStr.Replace("virtual", "override");
            
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
             }
             
             if (!Directory.Exists(viewsPath))
             {
                 Directory.CreateDirectory(viewsPath);
             }
             
             if (!File.Exists(viewFilePath))
             {
                 WriteFile(viewFilePath, viewStr);
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
            string[] asset = AssetDatabase.FindAssets("UnityController");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetViewTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("UnityView");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetRepositoryTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("UnityRepository");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetServiceTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("UnityService");
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