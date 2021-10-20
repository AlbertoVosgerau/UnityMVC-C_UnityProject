#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityMVC.Editor;
using UnityMVC.Model;
using UnityMVC.Utils;
using UnityMVCModuleModel = UnityMVC.Model.UnityMVCModuleModel;

namespace UnityMVC.CodeGenerator
{
    public enum ScriptType
    {
        MVCApplication,
        View,
        Controller,
        MVCComponent,
        UnityComponent,
        MVCComponentGroup,
        Container,
        Loader,
        Solver,
        Interface,
        Enum,
        ScriptableObject
    }
    
    public class MVCCodeGenerator
    {
        public static void CreateApplication(string projectName, string companyName)
        {
            
            MVCFolderStructure.SetupProjectFolder();
            GenerateApplicationMetadata($"{MVCFolderStructure.ProjectFolder}/Project.mvc", projectName, companyName);
            GenerateScript(null, projectName, GetTemplate(ScriptType.MVCApplication), MVCFolderStructure.ApplicationFolder, ScriptType.MVCApplication, true);
            if (MVCReflectionUtil.UsesAssemblyDefinition())
            {
                string commonName = GetModuleAssemlbyDefinitionName(companyName, projectName, "Common", false, true);
                CreateCoreAssemblyDefinition(MVCFolderStructure.CommonFolder, commonName);
                
                string commonRuntimeTests = GetModuleAssemlbyDefinitionName(companyName, projectName, "Common", true, true);
                CreatePlayModeAssemblyDefinition(MVCFolderStructure.CommonsPlayModeFolder, commonRuntimeTests);
                
                string commonEditorTests = GetModuleAssemlbyDefinitionName(companyName, projectName, "Common", true, false);
                CreateEditorModeAssemblyDefinition(MVCFolderStructure.CommonsEditModeFolder, commonEditorTests);
            }
            AssetDatabase.Refresh();
        }
        
        private static UnityMVCApplicationModel GenerateApplicationMetadata(string path, string applicationName, string companyName)
        {

            if (File.Exists(path))
            {
                return ReadModuleDataFromFolder(path);
            }

            UnityMVCApplicationModel data = new UnityMVCApplicationModel(applicationName, companyName);
            string str = JsonUtility.ToJson(data);
            
            MVCFileUtil.WriteFile(path, str);
            return data;
        }

        private static UnityMVCApplicationModel GetAppData()
        {
            return ReadModuleDataFromFile($"{MVCFolderStructure.ProjectFolder}/Project.mvc");
        }
        
        public static UnityMVCApplicationModel ReadModuleDataFromFolder(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return ReadMetadata(path);
        }
        
        public static UnityMVCApplicationModel ReadModuleDataFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return ReadMetadata(path);
        }
        
        private static UnityMVCApplicationModel ReadMetadata(string path)
        {
            string str = File.ReadAllText(path);
            UnityMVCApplicationModel data = JsonUtility.FromJson<UnityMVCApplicationModel>(str);
            return data;
        }

        public static void CreateView(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.View), GetPath("Views"), ScriptType.View, removeComments,inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.View, true), GetPath("Views"), ScriptType.View, removeComments, null, true, inheritsFrom, inheritsFrom != null);
        }

        public static void CreateController(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.Controller), GetPath("Controllers"), ScriptType.Controller, removeComments, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.Controller,true), GetPath("Controllers"), ScriptType.Controller, removeComments,null, true, inheritsFrom, inheritsFrom != null);
        }
        
        public static void CreateViewAndController(string nameSpace, string name, bool removeComments, string controllerInheritsFrom = null, string viewInheritsFrom = null)
        {
            CreateController(nameSpace, name, removeComments, controllerInheritsFrom);
            CreateView(nameSpace, name, removeComments, viewInheritsFrom);
        }
        public static void CreateComponent(string nameSpace, string name, bool removeComments, string view, string inheritsFrom = null)
        {
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.MVCComponent), GetPath("Components"), ScriptType.MVCComponent, removeComments, view, inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.MVCComponent,  true), GetPath("Components", true), ScriptType.MVCComponent, removeComments, view,true, inheritsFrom, inheritsFrom != null);
        }
        
        public static void CreateUnityComponent(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.UnityComponent), GetPath("UnityComponents"), ScriptType.UnityComponent, removeComments, null, inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null); }
        public static void CreateComponentGroup(string nameSpace, string name, bool removeComments, string view, string inheritsFrom = null)
        {
            GenerateScript(nameSpace, name, GetTemplate(ScriptType.MVCComponentGroup), GetPath("Components"), ScriptType.MVCComponentGroup, removeComments, view, inheritsFrom: inheritsFrom, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.MVCComponentGroup,  true), GetPath("Components", true), ScriptType.MVCComponentGroup, removeComments, view,true, inheritsFrom, inheritsFrom != null);
        }
        public static void CreateContainer(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Container), GetPath("Containers"), ScriptType.Container, removeComments, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Container, true), GetPath("Containers"), ScriptType.Container, removeComments, null,true, inheritsFrom, inheritsFrom != null);
        }
        public static void CreateLoader(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Loader), GetPath("Loaders"), ScriptType.Loader, removeComments, virtualToOverride: inheritsFrom != null);
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Loader, true), GetPath("Loaders"), ScriptType.Loader, removeComments, null, true, inheritsFrom, inheritsFrom != null);
        }
        
        public static void CreateSolver(string nameSpace, string name, bool removeComments, string inheritsFrom = null)
        {
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Solver), GetPath("Solvers"), ScriptType.Solver, removeComments, virtualToOverride: inheritsFrom != null, isPartial:false);
        }
        
        public static void CreateInterface(string nameSpace, string name, bool removeComments)
        {
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Interface), GetPath("Interfaces"), ScriptType.Interface, removeComments, virtualToOverride: false, isPartial:false);
        }
        
        public static void CreateEnum(string nameSpace, string name, bool removeComments)
        {
            GenerateScript(nameSpace,name, GetTemplate(ScriptType.Enum), GetPath("Enums"), ScriptType.Enum, removeComments, virtualToOverride: false, isPartial:false);
        }
        
        public static void CreateScriptableObject(string nameSpace, string name,int order, string filename, string menuName, bool removeComments)
        {
            string template = GetTemplate(ScriptType.ScriptableObject);
            template = template.Replace("666", order.ToString());
            template = template.Replace("{filename}", filename);
            template = template.Replace("{menuname}", menuName);
            GenerateScript(nameSpace,name, template, GetPath("ScriptableObjects"), ScriptType.ScriptableObject, removeComments, virtualToOverride: false, isPartial:false);
        }

        public static void UpdatePartial(string nameSpace, ScriptType type, string name, string baseType, string path, string view)
        {
            string template = GetTemplate(type, true);
            GenerateScript(nameSpace, name, template, path, type, true, isPartial: true, inheritsFrom: baseType, overrideFile: true, view: view);
        }

        private static void GenerateScript(string nameSpace, string name, string template, string path, ScriptType type, bool removeComments, string view = null, bool isPartial = false, string inheritsFrom = null, bool virtualToOverride = false, bool overrideFile = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            name = name.Replace(" ", "");

            string templateStr = template;
            string typeStr = type.ToString();
            templateStr = templateStr.Replace($"{typeStr}Template", $"{name}{typeStr}");
            templateStr = SolveBaseMethods(templateStr, inheritsFrom == null);
            
            string directoryPath = path;
            string filePath = isPartial? $"{directoryPath}/{name}{typeStr}Partial.cs" : $"{directoryPath}/{name}{typeStr}.cs";
            

            if (nameSpace != null)
            {
                templateStr = templateStr.Replace("/*<NAMESPACE>*/", $"namespace {nameSpace}\n{{");
                templateStr = templateStr.Replace("/*}*/", $"}}");
            }

            if (inheritsFrom != null)
            {
                templateStr = templateStr.Replace($"/*NEW*/", $"new");
                
                if (type == ScriptType.UnityComponent)
                {
                    ApplyInheritanceToUnityComponent(ref templateStr, inheritsFrom);
                }

                if (isPartial)
                {
                    ApplyInheritanceToPartial(ref templateStr, type, inheritsFrom);
                }
                else
                {
                    ApplyInheritanceToModel(ref templateStr, name,type, inheritsFrom);
                }
            }
            else
            {
                templateStr = templateStr.Replace($"/*NEW*/", $"");
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
                if(inheritsFrom == null)
                {
                    templateStr = templateStr.Replace($"ControllerTemplate", $"{name}Controller");
                    templateStr = templateStr.Replace($"ViewTemplateModel", $"{name}ViewModel");
                }
                else
                {
                    string inheritance = inheritsFrom.Replace("View", "");
                    templateStr = templateStr.Replace($"ControllerTemplate", $"{inheritance}Controller");
                    templateStr = templateStr.Replace($"ViewTemplateModel", $"{inheritance}ViewModel");

                    char[] filters = new[] {'#'};
                    List<string> eventsFilter = templateStr.Split(filters).ToList();
                    for (int i = 0; i < eventsFilter.Count; i++)
                    {
                        if (eventsFilter[i].Contains("public Action<View.View> onViewEnabled;"))
                        {
                            eventsFilter.Remove(eventsFilter[i]);
                            break;
                        }
                    }

                    templateStr = string.Join("", eventsFilter);
                }
                
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
            if (type == ScriptType.MVCComponentGroup)
            {
                templateStr = templateStr.Replace($"MVCComponentGroupTemplateModel", $"{name}ComponentGroupModel");
                
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
            
            if (type == ScriptType.Interface)
            {
                templateStr = templateStr.Replace($"InterfaceTemplate", $"I{name}");
            }

            if (type == ScriptType.ScriptableObject)
            {
                templateStr = templateStr.Replace($"ScriptableObjectTemplate", $"{name}");
            }
            
            if (type == ScriptType.Enum)
            {
                templateStr = templateStr.Replace($"EnumTemplate", $"{name}");
            }
            
            if (type == ScriptType.MVCApplication)
            {
                UnityMVCApplicationModel appData = GetAppData();
                
                if (MVCReflectionUtil.UsesAssemblyDefinition())
                {
                    string asmdefName = GetApplicationAssemlbyDefinitionName(appData.companyName, appData.applicationName,  false, true);
                    CreateCoreAssemblyDefinition(MVCFolderStructure.ApplicationFolder, asmdefName);
                }
                
                templateStr = templateStr.Replace($"ProjectNamespace", $"{appData.applicationName}");
                templateStr = templateStr.Replace($"MVCApplication", $"{appData.applicationName}Application");

                string codePath = $"{directoryPath}/Scripts";
                if (!Directory.Exists(codePath))
                {
                    Directory.CreateDirectory(codePath);
                }
                filePath = $"{codePath}/{appData.applicationName}Application.cs";
            }

            templateStr = templateStr.Replace($"ControllerTemplateEvents", $"{name}ControllerEvents");

            if (removeComments)
            {
                templateStr = StringWithoutComments(templateStr);
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath) || overrideFile)
            {
                MVCFileUtil.WriteFile(filePath, templateStr);
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
        
        private static void ApplyInheritanceToUnityComponent(ref string from, string inheritance)
        {
            from = from.Replace($": UnityComponent", $": {inheritance}");
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

        public static string GetMVCDataPath()
        {
            string[] asset = AssetDatabase.FindAssets("MVCData");
            string path = AssetDatabase.GUIDToAssetPath(asset[0]);
            List<string> directories = path.Split(new[] { "/", "\\" }, StringSplitOptions.None).ToList();
            directories.RemoveAt(directories.Count-1);
            path = String.Join("/", directories);
            return path;
        }
        
        private static string GetTemplate(ScriptType type, bool isPartial = false)
        {
            string templateName = isPartial? $"{type.ToString()}TemplatePartial" : $"{type.ToString()}Template";
            string[] assets = AssetDatabase.FindAssets(templateName);
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);
            
            return str;
        }

        private static string SolveBaseMethods(string str, bool removeBase)
        {
            if (!removeBase)
            {
                str = str.Replace("//base", "base");
                return str;
            }

            string[] lines = str.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var removedCommentsStr = lines.Where(x => !x.Contains("//base")).ToArray();

            str = String.Join("\n", removedCommentsStr);
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

            string assets = Application.dataPath;

            if (string.IsNullOrEmpty(UnityMVCResources.Data.CurrentScriptsFolder))
            {
                return $"{assets}/Scripts/{type}";
            }

            string folderPath = $"{assets}/{UnityMVCResources.Data.CurrentScriptsFolder}/{type}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);

            }
            return folderPath;
        }

        public static UnityMVCModuleModel CreateModule(string modulePath, string newModuleName, string newNamespace)
        {
            string absolutePath = $"{Application.dataPath}/{modulePath}/{newModuleName}";
            
            if (Directory.Exists(modulePath))
            {
                return UnityMVCModuleData.ReadModuleDataFromFolder(absolutePath);
            }
            
            Debug.Log($"Creating module at {absolutePath}");

            string scriptsFolder = $"{absolutePath}/Scripts";
            string testsFolder = $"{absolutePath}/Scripts/Tests";
            string playModeFolder = $"{absolutePath}/Scripts/Tests/PlayMode";
            string editModeFolder = $"{absolutePath}/Scripts/Tests/EditMode";
            string prefabsFolder = $"{absolutePath}/Prefabs";
            string scenesFolder = $"{absolutePath}/Scenes";

            Directory.CreateDirectory(absolutePath);
            Directory.CreateDirectory(prefabsFolder);
            Directory.CreateDirectory(scriptsFolder);
            Directory.CreateDirectory(scenesFolder);

            if (MVCReflectionUtil.UsesAssemblyDefinition())
            {
                Directory.CreateDirectory(testsFolder);
                Directory.CreateDirectory(playModeFolder);
                Directory.CreateDirectory(editModeFolder);

                UnityMVCApplicationModel appData = GetAppData();
                
                string moduleName = GetModuleAssemlbyDefinitionName(appData.companyName, appData.applicationName, newModuleName, false, true);
                CreateCoreAssemblyDefinition(scriptsFolder, moduleName);
                
                string moduleRuntime = GetModuleAssemlbyDefinitionName(appData.companyName, appData.applicationName, newModuleName, true, true);
                CreatePlayModeAssemblyDefinition(playModeFolder, moduleRuntime);
                
                string moduleEditor = GetModuleAssemlbyDefinitionName(appData.companyName, appData.applicationName, newModuleName, true, false);
                CreateEditorModeAssemblyDefinition(editModeFolder, moduleEditor);
            }

            UnityMVCModuleModel newModule =  UnityMVCModuleData.GenerateModuleMetadata(absolutePath, newModuleName, newNamespace);
            UnityMVCResources.Data.currentModule = newModule;
            
            CreateViewAndController(newNamespace, newModuleName, true);
            AssetDatabase.Refresh();
            
            return newModule;
        }
        
        
        public static void CreateAssetModule(string modulePath, string newModuleName)
        {
            string absolutePath = $"{Application.dataPath}/{modulePath}/{newModuleName}";
            
            if (Directory.Exists(modulePath))
            {
                return;
            }
            
            Debug.Log($"Creating module at {absolutePath}");

            List<string> paths = new List<string>();
            
            paths.Add($"{absolutePath}/3D Models");
            paths.Add($"{absolutePath}/Audio");
            paths.Add($"{absolutePath}/Materials");
            paths.Add($"{absolutePath}/Shaders");
            paths.Add($"{absolutePath}/Textures");
            paths.Add($"{absolutePath}/UI");
            paths.Add($"{absolutePath}/Sprites");
            paths.Add($"{absolutePath}/Prefabs");
            paths.Add($"{absolutePath}/Scenes");

            MVCFolderStructure.CreateDirectories(paths);

            AssetDatabase.Refresh();
        }

        private static void CreateCoreAssemblyDefinition(string path, string name)
        {
            string assemblyDefinitionTemplate = GetModuleAssemblyDefinitionTemplate();
            string GUID = MVCAssetDatabaseUtil.GetAssetGUID("AlbertoVosgerau.MVC");
            string assemblyDefinitionPath = $"{path}/{name}";
            List<string> guids = new List<string>() {GUID};
            
            CreateAssemblyDefinition(assemblyDefinitionPath, assemblyDefinitionTemplate, name, guids);
        }
        
        private static void CreatePlayModeAssemblyDefinition(string path, string name)
        {
            string assemblyDefinitionTemplate = GetPlayModeAssemblyDefinitionTemplate();
            string MvcGUID = MVCAssetDatabaseUtil.GetAssetGUID("AlbertoVosgerau.MVC");
            string assemblyDefinitionPath = $"{path}/{name}";
            List<string> guids = new List<string>() {MvcGUID};
            
            CreateAssemblyDefinition(assemblyDefinitionPath, assemblyDefinitionTemplate, name, guids);
        }
        
        private static void CreateEditorModeAssemblyDefinition(string path, string name)
        {
            string assemblyDefinitionTemplate = GetEditorTestAssemblyDefinitionTemplate();
            string MvcGUID = MVCAssetDatabaseUtil.GetAssetGUID("AlbertoVosgerau.MVC");
            string assemblyDefinitionPath = $"{path}/{name}";
            List<string> guids = new List<string>() {MvcGUID};
            
            CreateAssemblyDefinition(assemblyDefinitionPath, assemblyDefinitionTemplate, name, guids);
        }

        private static void CreateAssemblyDefinition(string path, string template, string name, List<string> guids)
        {
            for (int i = 0; i < guids.Count; i++)
            {
                guids[i] = GetGUIDLine(guids[i]);
            }

            guids.RemoveAll(x => string.IsNullOrWhiteSpace(x));
            
            string allGuids = string.Join(",", guids);
            allGuids = allGuids.Replace(",\"GUID:\"\"", "");
            allGuids = allGuids.Replace(",\"GUID:\"", "");
                
            string str = template.Replace("/*NAME*/", name);
            str = str.Replace("/*GUID*/", $"{allGuids}");
            
            MVCFileUtil.WriteFile(path, str);
        }

        private static string GetGUIDLine(string guid)
        {
            return $"\"GUID:{guid}\"";
        }

        private static string GetModuleAssemblyDefinitionTemplate()
        {
            string templateName = "ModuleAssemblyDefinitionTemplate";
            string[] assets = AssetDatabase.FindAssets(templateName);
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);
            
            return str;
        }
        
        private static string GetPlayModeAssemblyDefinitionTemplate()
        {
            string templateName = "PlayTestAssemblyDefinitionTemplate";
            string[] assets = AssetDatabase.FindAssets(templateName);
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);
            
            return str;
        }
        
        private static string GetEditorTestAssemblyDefinitionTemplate()
        {
            string templateName = "EditorTestAssemblyDefinitionTemplate";
            string[] assets = AssetDatabase.FindAssets(templateName);
            string path = AssetDatabase.GUIDToAssetPath(assets[0]);
            string str = File.ReadAllText(path);
            
            return str;
        }

        private static string GetModuleAssemlbyDefinitionName(string company, string project, string moduleName, bool isTest, bool isRuntime)
        {
            string name = $"{company}.{project}.{moduleName}";
            if (isTest)
            {
                name = isRuntime ? $"{name}.Runtime.Tests" : $"{name}.Editor.Tests";
            }
            return $"{name}.asmdef";
        }
        
        private static string GetApplicationAssemlbyDefinitionName(string company, string featureName, bool isTest, bool isRuntime)
        {
            string name = $"{company}.{featureName}";
            if (isTest)
            {
                name = isRuntime ? $"{name}.Runtime.Tests" : $"{name}.Editor.Tests";
            }
            return $"{name}.asmdef";
        }
    }
}
#endif