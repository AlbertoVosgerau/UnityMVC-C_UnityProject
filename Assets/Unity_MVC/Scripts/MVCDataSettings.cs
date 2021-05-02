using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace UnityMVC
{
    public class MVCDataSettings
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
            controllerStr.Replace("Controller", $"{name}Controller");
            controllerStr.Replace("View", $"{name}View");
            
            string viewStr = GetViewTemplate();
            viewStr.Replace("Controller", $"{name}Controller");
            viewStr.Replace("View", $"{name}View");
            
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
             
             if (!File.Exists(viewFilePath))
             {
                 WriteFile(viewFilePath, viewStr);
             }
        }

        private static void WriteFile(string path, string str)
        {
            // File.Create(path);
            // FileStream stream = new FileStream(path, FileMode.Open);
            // byte[] bytes = Encoding.UTF8.GetBytes(str);
            // stream.Write(bytes, 0, bytes.Length);
            // stream.Close();
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
            string[] asset = AssetDatabase.FindAssets("Controller");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetViewTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("View");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetRepositoryTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("Repository");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            string str = File.ReadAllText(path);
            return str;
        }
        
        private static string GetServiceTemplate()
        {
            string[] asset = AssetDatabase.FindAssets("Service");
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