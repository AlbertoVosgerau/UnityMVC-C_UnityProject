
using System.IO;
using UnityEngine;

namespace UnityMVC.Editor
{
    public class MVCFolderStructure
    {
        public static string ProjectFolder => $"{Application.dataPath}/_Project";
        public static string ThirdPartyFolder => $"{Application.dataPath}/ThirdParty";
        public static string ScriptsFolder => $"{ProjectFolder}/Scripts";
        public static string TestsFolder => $"{ProjectFolder}/Scripts/Tests";
        public static string ScenesFolder => $"{ProjectFolder}/Scenes";
        public static string PrefabsFolder => $"{ProjectFolder}/Prefabs";
        public static string CommonFolder => $"{ProjectFolder}/Common";
        public static string CommonsModules => $"{ProjectFolder}/Modules";
        public static string CommonsPrefabsFolder => $"{ProjectFolder}/Common/Prefabs";
        public static string CommonsScriptsFolder => $"{ProjectFolder}/Common/Scripts";
        public static string CommonsTestsFolder => $"{ProjectFolder}/Common/Scripts/Tests";
        public static string ApplicationFolder => $"{ProjectFolder}/Application/Scripts/Application";
        public static string ModulesFolder => $"{ProjectFolder}/Modules";
    
        public static bool FolderStructureIsOk()
        {
            bool isOk;

            isOk = Directory.Exists(ProjectFolder) &&
                   Directory.Exists(ScriptsFolder) &&
                   Directory.Exists(ScenesFolder) &&
                   Directory.Exists(PrefabsFolder) &&
                   Directory.Exists(CommonFolder) &&
                   Directory.Exists(CommonsModules) &&
                   Directory.Exists(CommonsPrefabsFolder) &&
                   Directory.Exists(CommonsScriptsFolder) &&
                   Directory.Exists(ThirdPartyFolder) &&
                   Directory.Exists(ApplicationFolder) &&
                   Directory.Exists(ModulesFolder);

            return isOk;
        }

        public static void SetupProjectFolder()
        {
            if (FolderStructureIsOk())
            {
                return;
            }
        
            if(!Directory.Exists(ProjectFolder))
            {
                Directory.CreateDirectory(ProjectFolder);
            }
        
            if(!Directory.Exists(ScriptsFolder))
            {
                Directory.CreateDirectory(ScriptsFolder);
            }
            
            if(!Directory.Exists(TestsFolder))
            {
                Directory.CreateDirectory(TestsFolder);
            }
        
            if(!Directory.Exists(ScenesFolder))
            {
                Directory.CreateDirectory(ScenesFolder);
            }
        
            if(!Directory.Exists(PrefabsFolder))
            {
                Directory.CreateDirectory(PrefabsFolder);
            }
        
            if(!Directory.Exists(CommonFolder))
            {
                Directory.CreateDirectory(CommonFolder);
            }
        
            if(!Directory.Exists(CommonsModules))
            {
                Directory.CreateDirectory(CommonsModules);
            }
        
            if(!Directory.Exists(CommonsPrefabsFolder))
            {
                Directory.CreateDirectory(CommonsPrefabsFolder);
            }
            
            if(!Directory.Exists(CommonsScriptsFolder))
            {
                Directory.CreateDirectory(CommonsScriptsFolder);
            }
            
            if(!Directory.Exists(CommonsTestsFolder))
            {
                Directory.CreateDirectory(CommonsTestsFolder);
            }

            if(!Directory.Exists(ThirdPartyFolder))
            {
                Directory.CreateDirectory(ThirdPartyFolder);
            }
        
            if(!Directory.Exists(ApplicationFolder))
            {
                Directory.CreateDirectory(ApplicationFolder);
            }
        
            if(!Directory.Exists(ModulesFolder))
            {
                Directory.CreateDirectory(ModulesFolder);
                string modulePath =  ModulesFolder.Substring(Application.dataPath.Length);
                if (modulePath.Length > 0)
                {
                    modulePath = modulePath.Remove(0, 1);
                }
                UnityMVCResources.SaveModulesPath(modulePath);
            }
        }
    }
}