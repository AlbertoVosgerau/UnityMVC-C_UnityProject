#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnityMVC.Editor
{
    public class MVCFolderStructure
    {
        public static string ProjectFolder => $"{Application.dataPath}/_Project";
        public static string ThirdPartyFolder => $"{Application.dataPath}/ThirdParty";
        public static string ScenesFolder => $"{ProjectFolder}/Scenes";
        public static string CommonFolder => $"{ProjectFolder}/Common";
        
        public static string ModulesRealativeFolder => $"_Project/Modules";
        public static string AssetModulesRelativeFolder => $"_Project/AssetModules";
        
        public static string MaterialsFolder => $"{ProjectFolder}/Common/Materials";
        public static string CommonsModules => $"{ProjectFolder}/Modules";
        public static string CommonsPrefabsFolder => $"{ProjectFolder}/Common/Prefabs";
        public static string CommonsScriptsFolder => $"{ProjectFolder}/Common/Scripts";
        public static string CommonsTestsFolder => $"{ProjectFolder}/Common/Scripts/Tests";
        public static string CommonsPlayModeFolder => $"{ProjectFolder}/Common/Scripts/Tests/PlayMode";
        public static string CommonsEditModeFolder => $"{ProjectFolder}/Common/Scripts/Tests/EditMode";
        public static string ApplicationFolder => $"{ProjectFolder}/Application";
        public static string ModulesFolder => $"{ProjectFolder}/Modules";
        public static string AssetModulesFolder => $"{Application.dataPath}/_Project/AssetModules";
        
        public static string Models3DFolder => $"{Application.dataPath}/_Project/Common/3D Models";
        public static string ShadersFolder => $"{Application.dataPath}/_Project/Common/Shaders";
        public static string AudioFolder => $"{Application.dataPath}/_Project/Common/Audio";
        public static string ResourcesFolder => $"{Application.dataPath}/_Project/Resources";
        public static string SpritesFolder => $"{Application.dataPath}/_Project/Common/Sprites";
        public static string TexturesFolder => $"{Application.dataPath}/_Project/Common/Textures";
        public static string UIFolder => $"{Application.dataPath}/_Project/Common/UI";

        public static bool create3dModelsFolder = false;
        public static bool createAudioFolder = false;
        public static bool createResourcesFolder = false;
        public static bool createSpritesFolder = false;
        public static bool createTexturesFolder = false;
        public static bool createUIFolder = false;
        public static bool createThirdPartyFolder = true;
        public static bool createShadersFolder = false;

        public static bool FolderStructureIsOk()
        {
            bool isOk;

            isOk = Directory.Exists(ProjectFolder) &&
                   Directory.Exists(ScenesFolder) &&
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
            

            if(!Directory.Exists(ScenesFolder))
            {
                Directory.CreateDirectory(ScenesFolder);
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

            if (createThirdPartyFolder)
            {
                if(!Directory.Exists(ThirdPartyFolder))
                {
                    Directory.CreateDirectory(ThirdPartyFolder);
                }
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
            

            
            if (create3dModelsFolder)
            {
                if(!Directory.Exists(Models3DFolder))
                {
                    Directory.CreateDirectory(Models3DFolder);
                }
            }
            
            if (createAudioFolder)
            {
                if(!Directory.Exists(AudioFolder))
                {
                    Directory.CreateDirectory(AudioFolder);
                }
            }
            
            if (createResourcesFolder)
            {
                if(!Directory.Exists(ResourcesFolder))
                {
                    Directory.CreateDirectory(ResourcesFolder);
                }
            }
            
            if (createSpritesFolder)
            {
                if(!Directory.Exists(SpritesFolder))
                {
                    Directory.CreateDirectory(SpritesFolder);
                }
            }
            
            if (createShadersFolder)
            {
                if(!Directory.Exists(ShadersFolder))
                {
                    Directory.CreateDirectory(ShadersFolder);
                }
            }
            
            if (createTexturesFolder)
            {
                if(!Directory.Exists(TexturesFolder))
                {
                    Directory.CreateDirectory(TexturesFolder);
                }
            }
            
            if (createUIFolder)
            {
                if(!Directory.Exists(UIFolder))
                {
                    Directory.CreateDirectory(UIFolder);
                }
            }

            if (!Directory.Exists(MaterialsFolder))
            {
                Directory.CreateDirectory(MaterialsFolder);
            }
            
            if (!Directory.Exists(AssetModulesFolder))
            {
                Directory.CreateDirectory(AssetModulesFolder);
            }
            
            UnityMVCResources.SaveModulesPath(ModulesRealativeFolder);
            UnityMVCResources.SaveAssetModulesPath(AssetModulesRelativeFolder);
            UnityMVCResources.SaveAllData();

            if (!MVCReflectionUtil.UsesAssemblyDefinition())
            {
                return;
            }

            if(!Directory.Exists(CommonsTestsFolder))
            {
                Directory.CreateDirectory(CommonsTestsFolder);
            }
            
            if(!Directory.Exists(CommonsPlayModeFolder))
            {
                Directory.CreateDirectory(CommonsPlayModeFolder);
            }
            
            if(!Directory.Exists(CommonsEditModeFolder))
            {
                Directory.CreateDirectory(CommonsEditModeFolder);
            }

        }

        public static void CreateDirectories(List<string> paths)
        {
            foreach (string path in paths)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}
#endif