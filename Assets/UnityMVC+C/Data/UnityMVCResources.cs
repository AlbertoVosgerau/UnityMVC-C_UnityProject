using System;
using UnityEngine;

namespace UnityMVC.Editor
{
    [Serializable]
    public class UnityMVCDataModel
    {
        public const string MODULES_RELATIVE_PATH_VARIABLE = "MVC_ModulesRelativePath";
        public const string REMOVECOMMENTS_VARIABLE = "MVC_RemoveComments";

        public string CurrentScriptsFolder => currentModule == null? Application.dataPath : $"{modulesRelativePath}/{currentModule.moduleName}/Scripts";
        
        public UnityMVCModuleModel currentModule;
        
        public string modulesRelativePath;
        public bool removeComments;
    }

    public class UnityMVCResources
    {
        public static UnityMVCDataModel Data => _data;
        private static UnityMVCDataModel _data = new UnityMVCDataModel();

        public static void LoadData()
        {
            if (!PlayerPrefs.HasKey(UnityMVCDataModel.MODULES_RELATIVE_PATH_VARIABLE))
            {
                PlayerPrefs.SetString(UnityMVCDataModel.MODULES_RELATIVE_PATH_VARIABLE, MVCFolderStructure.ModulesFolder);
            }
            if (!PlayerPrefs.HasKey(UnityMVCDataModel.REMOVECOMMENTS_VARIABLE))
            {
                PlayerPrefs.SetInt(UnityMVCDataModel.REMOVECOMMENTS_VARIABLE, 0);
            }
            
            _data.modulesRelativePath = PlayerPrefs.GetString(UnityMVCDataModel.MODULES_RELATIVE_PATH_VARIABLE);
            _data.removeComments = PlayerPrefs.GetInt(UnityMVCDataModel.REMOVECOMMENTS_VARIABLE) != 0;
        }

        public static void SaveAllData()
        {
            PlayerPrefs.SetString(UnityMVCDataModel.MODULES_RELATIVE_PATH_VARIABLE, _data.modulesRelativePath);
            int removeComments = _data.removeComments ? 1 : 0;
            PlayerPrefs.SetInt(UnityMVCDataModel.REMOVECOMMENTS_VARIABLE, removeComments);
        }

        public static void SaveModulesPath(string path)
        {
            _data.modulesRelativePath = path;
            PlayerPrefs.SetString(UnityMVCDataModel.MODULES_RELATIVE_PATH_VARIABLE, path);
        }
        
        public static void SaveRemoveComments(bool removeComments)
        {
            _data.removeComments = removeComments;
            int intValue = _data.removeComments ? 1 : 0;
            PlayerPrefs.SetInt(UnityMVCDataModel.REMOVECOMMENTS_VARIABLE, intValue);
        }

        public static string GetScriptsPath(string moduleName)
        {
            string path = $"{GetAbsoluteModulePath(_data.modulesRelativePath, moduleName)}/Scripts";
            return path;
        }
        
        public static string GetAbsoluteModulePath(string relativeModulePath, string moduleName)
        {
            string path = $"{GetAbsoluteModulesPath(relativeModulePath)}/{moduleName}";
            return path;
        }

        public static string GetAbsoluteModulesPath(string relativeModulePath)
        {
            string path = $"{Application.dataPath}/{relativeModulePath}";
            return path;
        }
    }
}