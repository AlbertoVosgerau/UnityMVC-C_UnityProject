
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
        public static string PlayModeFolder => $"{ProjectFolder}/Scripts/Tests/PlayMode";
        public static string EditModeFolder => $"{ProjectFolder}/Scripts/Tests/EditMode";
        public static string ScenesFolder => $"{ProjectFolder}/Scenes";
        public static string PrefabsFolder => $"{ProjectFolder}/Prefabs";
        public static string CommonFolder => $"{ProjectFolder}/Common";
        public static string CommonsModules => $"{ProjectFolder}/Modules";
        public static string CommonsPrefabsFolder => $"{ProjectFolder}/Common/Prefabs";
        public static string CommonsScriptsFolder => $"{ProjectFolder}/Common/Scripts";
        public static string CommonsTestsFolder => $"{ProjectFolder}/Common/Scripts/Tests";
        public static string CommonsPlayModeFolder => $"{ProjectFolder}/Common/Scripts/Tests/PlayMode";
        public static string CommonsEditModeFolder => $"{ProjectFolder}/Common/Scripts/Tests/EditMode";
        public static string ApplicationFolder => $"{ProjectFolder}/Application/Scripts/Application";
        public static string ModulesFolder => $"{ProjectFolder}/Modules";
        
        public static string Models3DFolder => $"{Application.dataPath}/_Project/3D Models";
        public static string AudioFolder => $"{Application.dataPath}/_Project/Audio";
        public static string ResourcesFolder => $"{Application.dataPath}/_Project/Resources";
        public static string SpritesFolder => $"{Application.dataPath}/_Project/Sprites";
        public static string TexturesFolder => $"{Application.dataPath}/_Project/Textures";
        public static string UIFolder => $"{Application.dataPath}/_Project/UI";

        public static bool create3dModelsFolder = false;
        public static bool createAudioFolder = false;
        public static bool createResourcesFolder = false;
        public static bool createSpritesFolder = false;
        public static bool createTexturesFolder = false;
        public static bool createUIFolder = false;
        public static bool createThirdPartyFolder = true;
    
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
            
            if(!Directory.Exists(PlayModeFolder))
            {
                Directory.CreateDirectory(PlayModeFolder);
            }
            
            if(!Directory.Exists(EditModeFolder))
            {
                Directory.CreateDirectory(EditModeFolder);
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
            
            if(!Directory.Exists(CommonsPlayModeFolder))
            {
                Directory.CreateDirectory(CommonsPlayModeFolder);
            }
            
            if(!Directory.Exists(CommonsEditModeFolder))
            {
                Directory.CreateDirectory(CommonsEditModeFolder);
            }
            
            if(!Directory.Exists(CommonsTestsFolder))
            {
                Directory.CreateDirectory(CommonsTestsFolder);
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
        }
    }
}