#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityMVC.CodeGenerator;
using UnityMVC.Component;
using UnityMVC.Inspector;
using UnityMVC.Model;

namespace UnityMVC.Editor
{
#pragma warning disable 414
    public class MVCCreateWindows : EditorWindow
    {
        private int _mainTabIntex = 0;
        private string[] _mainTabs = new[] {"Module Wizard", "Code Generator", "Inspector", "Assets"};

        private string _newModuleName;
        private string _newNamespace;

        private string _modulePath;
        private string _assetModulespath;
        private string _newAssetModule;
        
        private string _namespacePrefix = "";
        private string _namespace = "";
        private string _baseName = "";
        private string _projectName = "";
        private string _companyName = "";
        private float _btnWidth = 220;
        private Vector2 _scrollPosition = new Vector2(0,0);
        private int _currentMVCTab = 0;
        private string[] _MVCtabs = new[] {"Controllers/Views", "MVC Components", "Models", "UnityComponent", "Other"};
        private int _currentPath = 0;
        private List<string> _dataPaths;
        

        private List<string> _controllerAndViewTypes = new List<string>();
        private int _controllerAndViewTypeIndex;
        private List<string> _loaderSolverAndContainerTypes = new List<string>();
        private int _loaderSolverAndContainerIntex;
        private List<string> _controllerTypes = new List<string>();
        private int _controllerTypeIndex;
        private List<string> _viewTypes = new List<string>();
        private int _viewTypeIndex;
        private List<string> _componentTypes = new List<string>();
        private int _componentTypeIndex;
        private List<string> _containerTypes = new List<string>();
        private int _containerTypeIndex;
        private List<string> _loaderTypes = new List<string>();
        private int _loaderTypeIndex;
        private List<string> _solverTypes = new List<string>();
        private int _solverTypeIndex;
        private List<string> _unityComponentTypes = new List<string>();
        private int _unityComponentIndex;

        private int _moduleIndex;
        private List<UnityMVCModuleModel> _modules = new List<UnityMVCModuleModel>();

        private List<MVCDependencyResult> _dependenciesList = new List<MVCDependencyResult>();
        private List<bool> _dependenciesFoldout = new List<bool>();

        private List<string> _componentViewTypes = new List<string>();
        private int _componentViewIndex;
        private bool _hasApplication = false;

        int _scriptableObjectOrder = 0;
        string _scriptableObjectFileName;
        private string _scriptableObjectMenuName;

        
        [MenuItem("Unity MVC+C/Open Creation Window", priority = 0)]
        private static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC+C Generator");
            window.Show();
        }

        private void OnEnable()
        {
            Refresh();
        }

        private void Refresh()
        {
            _projectName = Application.productName;
            _companyName = Application.companyName;
            _hasApplication = HasApplication();
            //SolveDatapaths();
            UnityMVCResources.LoadData();
            _modulePath = UnityMVCResources.Data.modulesRelativePath;
            _assetModulespath = UnityMVCResources.Data.assetModulesRelativePath;
            UnityMVCModuleData.UpdateModulesList();
            UpdateAllTypes();
            UpdateDependencies();
        }

        private void UpdateDependencies()
        {
            _dependenciesFoldout.Clear();
            foreach (UnityMVCModuleModel module in _modules)
            {
                _dependenciesFoldout.Add(false);
                _dependenciesList.Add(MVCInspector.GetDependenciesList(module.moduleNamespace));
            }
        }

        private void SolveDatapaths()
        {
            _dataPaths = AssetDatabase.GetAllAssetPaths().ToList();
            
            foreach (string path in _dataPaths)
            {
                List<string> foldersList = path.Split('/', '\\').ToList();
                foldersList.Remove(foldersList.Last());
                String.Join(path, foldersList);
            }
        }

        void OnGUI()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true,GUIStyle.none, GUI.skin.verticalScrollbar);
            BuildInspector();
            GUILayout.EndScrollView();
        }

        private void BuildInspector()
        {
            if (!_hasApplication)
            {
                CreateApplicationArea();
                return;
            }
            MainTabs();
        }

        private static void Header(string str)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(str, EditorStyles.boldLabel, GUILayout.Width(440));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void CreateApplicationArea()
        {
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Let's create your MVC+C Project!", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Create the base name for your project below.", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            _projectName = EditorGUILayout.TextField("Project name", _projectName, GUILayout.Width(_btnWidth * 2));

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            _companyName = EditorGUILayout.TextField("Company name", _companyName, GUILayout.Width(_btnWidth * 2));

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label($"This will create the project structure and your {_projectName}MVCApplication.", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label($"It will be found under _Project/Commons/Application/Scripts.", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _projectName = _projectName.Replace(" ", "");
            _companyName = _companyName.Replace(" ", "");
            

            if (GUILayout.Button($"Create MVCApplication", GUILayout.Width(_btnWidth * 2)))
            {
                if (string.IsNullOrWhiteSpace(_projectName))
                {
                    ShowNameEmptyDialog();
                    return;
                }

                if (string.IsNullOrWhiteSpace(_companyName) || _companyName == "DefaultCompany")
                {
                    ShowCompanyNameEmptyDialog();
                    return;
                }
                
                MVCCodeGenerator.CreateApplication(_projectName, _companyName);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(5);
            OptionsArea();
            GUILayout.Space(15);
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label($"The project {_projectName} will be created with the following folder structure", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            FoldersCreationArea();
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        private void FoldersCreationArea()
        {
            GUILayout.BeginVertical();

            Checkbox(true, "_Project", 0);
            Checkbox(true,"Application", 1);
            Checkbox(true,"AssetModules", 1);
            Checkbox(true,"Common", 1);
            Checkbox(ref MVCFolderStructure.create3dModelsFolder,"3D Models", 2);
            Checkbox(ref MVCFolderStructure.createAudioFolder,"Audio", 2);
            Checkbox(true,"Materials", 2);
            Checkbox(true,"Prefabs", 2);
            Checkbox(true,"Scripts", 2);
            Checkbox(true,"Tests", 3);
            Checkbox(true,"EditMode", 4);
            Checkbox(true,"PlayMode", 4);
            Checkbox(ref MVCFolderStructure.createShadersFolder,"Shaders", 2);
            Checkbox(ref MVCFolderStructure.createSpritesFolder,"Sprites", 2);
            Checkbox(ref MVCFolderStructure.createTexturesFolder,"Textures", 2);
            Checkbox(ref MVCFolderStructure.createUIFolder,"UI", 2);
            Checkbox(true,"Modules", 1);
            Checkbox(ref MVCFolderStructure.createResourcesFolder,"Resources", 1);
            Checkbox(true,"Scenes", 1);

            Checkbox(ref MVCFolderStructure.createThirdPartyFolder,"ThirdParty", 0);

            GUILayout.EndVertical();
        }

        private void Checkbox(ref bool value, string str, int spacing)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Space(spacing * 20);

            value = GUILayout.Toggle(value, str, GUILayout.Width(_btnWidth *2));
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void Checkbox(bool defaultValue, string str, int spacing)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Space(spacing * 20);
            GUIStyle style = new GUIStyle("Toggle");
            style.fontStyle = FontStyle.Bold;

            GUILayout.Toggle(defaultValue, str, style, GUILayout.Width(_btnWidth *2));
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void MainTabs()
        {
            if (_modules.Count == 0)
            {
                NoModuleFoundArea();
                ModuleWizardArea();
                return;
            }
            
            _mainTabIntex = GUILayout.Toolbar(_mainTabIntex, _mainTabs);
            
            GUILayout.Space(10);
            
            switch (_mainTabIntex)
            {
                case 0:
                    ModuleWizardArea();
                    break;
                case 1:
                    MVCAreaCodeGeneratorArea();
                    break;
                case 2:
                    InspectorArea();
                    break;
                case 3:
                    AssetsArea();
                    break;
            }
        }

        private void AssetsArea()
        {
            GUILayout.BeginVertical();
            CreateAssetModuleArea();
            GUILayout.EndVertical();
        }

        private void HelpArea()
        {
            GUILayout.Label($"Will contain Help and documentation");
        }

        private void MVCTabs()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            GUIStyle style = new GUIStyle(GUI.skin.button);;
            style.fontSize = 2;

            _currentMVCTab = GUILayout.Toolbar(_currentMVCTab, _MVCtabs, style );
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            switch (_currentMVCTab)
            {
                case 0:
                    ViewAndControllerArea();
                    break;
                case 1:
                    if (_componentViewTypes.Count > 0)
                    {
                        ComponentArea();
                    }
                    break;
                case 2:
                    ModelsArea();
                    break;
                case 3:
                    UnityComponentArea();
                    break;
                case 4:
                    OtherArea();
                    break;
            }
        }

        private void NoModuleFoundArea()
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("No module found. Please create your first module!", GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        private void ModuleWizardArea()
        {
            GUILayout.BeginVertical();
            CreateCodeModuleWizardArea();
            GUILayout.EndVertical();
        }

        private void CreateCodeModuleWizardArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var labelStyle = new GUIStyle("Label");
            labelStyle.fontSize = 20;
            GUILayout.Label(new GUIContent($"Create full code module"), labelStyle, GUILayout.Width(_btnWidth + 225));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(15);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            GUILayout.Label(new GUIContent($"Modules path: Assets/{_modulePath}", $"Assets/{_modulePath}"), GUILayout.Width(_btnWidth + 185));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }
            if (GUILayout.Button("...", GUILayout.Width(35)))
            {
                string pathToOpen = Application.dataPath;
                _modulePath = EditorUtility.OpenFolderPanel("Select folder", pathToOpen, $"{Application.dataPath}/{UnityMVCResources.Data.modulesRelativePath}");
                
                if (PathIsValid(_modulePath)) {
                    _modulePath =  _modulePath.Substring(Application.dataPath.Length);
                    if (_modulePath.Length > 0)
                    {
                        _modulePath = _modulePath.Remove(0, 1);
                    }
                    UnityMVCResources.SaveModulesPath(_modulePath);
                }
                else
                {
                    if (!String.IsNullOrEmpty(_modulePath))
                    {
                        ShowInvalidPathDialog();
                        return;
                    }
                }
                OnChangedValue();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _newModuleName = EditorGUILayout.TextField("Module name:", _newModuleName, GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _newNamespace = EditorGUILayout.TextField("Namespace", _newNamespace, GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            OptionsArea();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"Create Module", GUILayout.Width(_btnWidth * 2)))
            {
                if (String.IsNullOrEmpty(_newModuleName))
                {
                    ShowNameEmptyDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_newNamespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }
                
                UpdateModules(ref _modules, typeof(View.View));

                foreach (UnityMVCModuleModel model in _modules)
                {
                    if (model.moduleName == _newModuleName)
                    {
                        ShowModuleAlreadyExistsDialog();
                        return;
                    }
                }

                UnityMVCModuleModel newMetadata =  MVCCodeGenerator.CreateModule(_modulePath, _newModuleName, _newNamespace);
                UnityMVCResources.Data.currentModule = newMetadata;

                _modulePath = String.Empty;
                _newModuleName = String.Empty;
                _newNamespace = String.Empty;

                OnCreatedFile();
                OnChangedValue();
                _mainTabIntex = 1;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
        private void CreateAssetModuleArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var labelStyle = new GUIStyle("Label");
            labelStyle.fontSize = 20;
            GUILayout.Label(new GUIContent($"Create asset module"), labelStyle, GUILayout.Width(_btnWidth + 225));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(15);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            GUILayout.Label(new GUIContent($"Asset Modules path: Assets/{_assetModulespath}", $"Assets/{_assetModulespath}"), GUILayout.Width(_btnWidth + 185));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }
            if (GUILayout.Button("...", GUILayout.Width(35)))
            {
                string pathToOpen = Application.dataPath;
                _assetModulespath = EditorUtility.OpenFolderPanel("Select folder", pathToOpen, $"{Application.dataPath}/{UnityMVCResources.Data.assetModulesRelativePath}");
                
                if (PathIsValid(_assetModulespath)) {
                    _assetModulespath =  _assetModulespath.Substring(Application.dataPath.Length);
                    if (_assetModulespath.Length > 0)
                    {
                        _assetModulespath = _assetModulespath.Remove(0, 1);
                    }
                    UnityMVCResources.SaveAssetModulesPath(_assetModulespath);
                }
                else
                {
                    if (!String.IsNullOrEmpty(_assetModulespath))
                    {
                        ShowInvalidPathDialog();
                        return;
                    }
                }
                OnChangedValue();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            _newAssetModule = EditorGUILayout.TextField("Module name:", _newAssetModule, GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"Create Asset Module", GUILayout.Width(_btnWidth * 2)))
            {
                if (String.IsNullOrEmpty(_newAssetModule))
                {
                    ShowNameEmptyDialog();
                    return;
                }

                MVCCodeGenerator.CreateAssetModule(_assetModulespath, _newAssetModule);

                _newAssetModule = String.Empty;

                OnCreatedFile();
                OnChangedValue();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void MVCAreaCodeGeneratorArea()
        {
            Header("Create MVC+C Script");
            GUILayout.Space(20);
            
            CreateAssetAtArea();
            GUILayout.Space(10);
            
            NamespaceNameArea();
            BaseFileNameArea();
            
            GUILayout.BeginVertical();
            GUILayout.Space(5);

            if (!_hasApplication)
            {
                GUILayout.Space(15);
                CreateApplicationArea();
            }
            else
            {
                OptionsArea();
                GUILayout.Space(20);
                MVCTabs();
            }
            
            GUILayout.EndVertical();
        }

        private void SettingsArea()
        {
            Header("MVC+C Settings");
            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("All settings on this session are placeholder for now. Nothing is really working.");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);
            OptionsArea();
        }

        private void OptionsArea()
        {
            GUILayout.BeginVertical();
            RemoveCommentsFromGeneratedCodeArea();
            GUILayout.EndVertical();
        }

        private void CreateAssetAtArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            string creationPath = $"Assets/{UnityMVCResources.Data.CurrentScriptsFolder}";
            GUILayout.Label(new GUIContent($"Files will be created at:  {creationPath}", $"{creationPath}"), GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
        private void InspectorArea()
        {
            GUILayout.Label($"Dependency inspector. Maps MVC+C modules that depends on each other\n\nThe warnings help you to find possible unintended dependencies.\nIf you are sure your code is ok, just ignore it.", GUILayout.Width(_btnWidth * 2));
            GUILayout.Space(20);

            for (int i = 0; i < _dependenciesList.Count; i++)
            {
                MVCDependencyResult dependencyInfo = _dependenciesList[i];

                int dependenciesCount = dependencyInfo.controllers.ItemsCount +
                            dependencyInfo.mvcComponentGroups.ItemsCount +
                            dependencyInfo.views.ItemsCount +
                            dependencyInfo.mvcComponents.ItemsCount +
                            dependencyInfo.unityComponents.ItemsCount;
                
                int classesCount = dependencyInfo.controllers.Results.Count +
                                   dependencyInfo.mvcComponentGroups.Results.Count +
                                   dependencyInfo.views.Results.Count +
                                   dependencyInfo.mvcComponents.Results.Count +
                                   dependencyInfo.unityComponents.Results.Count;

                string name = dependencyInfo.controllers.Results[0].type.Name.Replace("Controller", "");

                GUIContent icon = dependencyInfo.IsOk? EditorGUIUtility.IconContent("d_winbtn_mac_max") : EditorGUIUtility.IconContent("d_console.warnicon.sml");

                string text = $"<b>{name} Module</b>:  {classesCount.ToString("00")} MVC+C classes and {dependenciesCount.ToString("00")} MVC+C dependencies";


                GUILayout.BeginHorizontal();
                GUILayout.Space(5);
                GUILayout.Label(icon, GUIStyle.none);
                if (dependenciesCount == 0)
                {
                    GUIStyle textStyle = new GUIStyle();
                    textStyle.richText = true;
                    textStyle.normal.textColor =new Color(0.8f, 0.8f, 0.8f);
                    EditorGUILayout.LabelField($"   {text}", textStyle);
                }
                else
                {
                    GUIStyle foldoutStyle = new GUIStyle("Foldout");
                    foldoutStyle.richText = true;
                    foldoutStyle.normal.textColor =new Color(0.8f, 0.8f, 0.8f);
                    
                    _dependenciesFoldout[i] = EditorGUILayout.Foldout(_dependenciesFoldout[i], text, foldoutStyle);
                    
                    if (_dependenciesFoldout[i])
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(-55);
                        GUILayout.BeginVertical();
                        GUILayout.Space(25);

                        DependencyFeedback(dependencyInfo.controllers.Results);
                        DependencyFeedback(dependencyInfo.mvcComponentGroups.Results);
                        DependencyFeedback(dependencyInfo.views.Results);
                        DependencyFeedback(dependencyInfo.mvcComponents.Results);
                        DependencyFeedback(dependencyInfo.unityComponents.Results);
                        
                        GUILayout.EndVertical();
                        GUILayout.FlexibleSpace();
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    }
                }
                
                
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.Space(15);
            }
        }


        private void DependencyFeedback(List<MVCInspectorDataTypeResult> results)
        {
            foreach (var dependency in results)
            {
                if (dependency.fieldInfos.Count == 0)
                {
                    return;
                }
                
                GUILayout.Label($"{dependency.type.Name} depends on:", GUILayout.Width(_btnWidth * 2));
                GUILayout.Space(5);

                for (int i = 0; i < dependency.fieldInfos.Count; i++)
                {
                    FieldInfo info = dependency.fieldInfos[i];
                    MessageType message = dependency.MessageTypes[i];
                    char[] separators = new char[] { '.' };
                    string[] name = info.FieldType.ToString().Split(separators);
                    GUIStyle fakeHelpbox = new GUIStyle("HelpBox");
                    fakeHelpbox.fontSize = 13;
                    
                    
                    if (message == MessageType.Info)
                    {
                        GUIContent icon = new GUIContent($" {name.Last()} on variable {info.Name}", EditorGUIUtility.IconContent("d_winbtn_mac_max").image);

                        EditorGUILayout.LabelField(icon, fakeHelpbox);
                    }
                    else
                    {
                        string toolTip = InspectorFeedbackTooltip(dependency.type.BaseType);
                        
                        GUIContent icon = new GUIContent($" {name.Last()} on variable {info.Name}", EditorGUIUtility.IconContent("d_console.warnicon.sml").image, tooltip: toolTip);
                        EditorGUILayout.LabelField(new GUIContent(icon), fakeHelpbox);
                    }
                    GUILayout.Space(5);
                }
                
                GUILayout.Space(20);
            }
        }

        private string InspectorFeedbackTooltip(Type type)
        {
            if (type == typeof(Controller.Controller))
            {
                return "Controllers are allowed to have only other module's Controller as field dependency.\n" +
                       "If you need to send other type of data, consider using events instead.";
            }
            if (type == typeof(View.View))
            {
                return "Views are not allowed to use ANY field dependency from other modules. Its own Controller should provide the needed data.";
            }
            if (type == typeof(MVCComponent))
            {
                return "MVCComponents are not allowed to use ANY field dependency from other modules. Its own View should provide the needed data.";
            }
            if (type == typeof(MVCComponentGroup))
            {
                return "MVCComponentGroups are only allowed to depend on MVCComponents from other modules.";
            }
            if (type == typeof(UnityComponent.UnityComponent))
            {
                return "MVCComponents are not allowed to use ANY field dependency from other modules. A MVCComponent should provide the needed data.";
            }

            return "You have a dependency from another module. This can be an unintended dependency, please check your script.";
        }
        
        private void NamespacePrefixArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _namespacePrefix = EditorGUILayout.TextField("Namespace prefix", _namespacePrefix, GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
        private void NamespaceNameArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            List<string> names = _modules.Select(x => x.moduleName).ToList();
            ModulesDropdown(ref _moduleIndex, names);
            _namespace = _modules[_moduleIndex].moduleNamespace;
            UnityMVCResources.Data.currentModule = _modules[_moduleIndex];
            if (EditorGUI.EndChangeCheck())
            {
                UpdateAllTypes();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        private void BaseFileNameArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _baseName = EditorGUILayout.TextField("Base File Name", _baseName, GUILayout.Width(_btnWidth * 2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void RemoveCommentsFromGeneratedCodeArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            Checkbox(ref UnityMVCResources.Data.removeComments, " Remove comments from generated code", 0);
            UnityMVCResources.SaveAllData();
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ViewAndControllerArea()
        {
            HeaderCreate("View/Controller");

            ViewAndControllerButton();
            SingleTypesDropdown(ref _controllerAndViewTypeIndex, _controllerAndViewTypes);
            GUILayout.Space(10);

            ViewAndControllerButtons();
            DoubleTypesDropdown(ref _viewTypeIndex, _viewTypes, ref _controllerTypeIndex, _controllerTypes);
        }
        private void ComponentArea()
        {
            HeaderCreate("MVC Component");
            
            ComponentButton();
            CustomSingleTypesDropdown(ref _componentTypeIndex, _componentTypes, "and inherits from:");
        }
        private void ModelsArea()
        {
            HeaderCreate("Loader, Solver and Controller");
            
            LoaderSolverAndContainerButton();
            SingleTypesDropdown(ref _loaderSolverAndContainerIntex, _loaderSolverAndContainerTypes);
            GUILayout.Space(7);
            
            HeaderCreate("Container");
            
            ContainerButton();
            SingleTypesDropdown(ref _containerTypeIndex, _containerTypes);
            GUILayout.Space(7);
            
            HeaderCreate("Loader and Solver");
            
            LoaderAndSolverButtons();
            DoubleTypesDropdown(ref  _loaderTypeIndex, _loaderTypes, ref _solverTypeIndex, _solverTypes);
        }
        
        private void OtherArea()
        {

            CreateInterfaceButton();
            CreateEnumButton();
            CreateScriptableObjectButton();
        }

        private void CreateInterfaceButton()
        {
            HeaderCreate("Interface");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"I{_baseName}", GUILayout.Width(_btnWidth * 2)))
            {
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }
                
                MVCCodeGenerator.CreateInterface(_namespace, _baseName, UnityMVCResources.Data.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void CreateEnumButton()
        {
            GUILayout.Space(5);
            HeaderCreate("Enum");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"{_baseName}", GUILayout.Width(_btnWidth * 2)))
            {
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }
                
                MVCCodeGenerator.CreateEnum(_namespace, _baseName, UnityMVCResources.Data.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void CreateScriptableObjectButton()
        {
            GUILayout.Space(5);
            HeaderCreate("Scriptable Object");
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            int order = 0;
            GUILayout.Label("Order: ");
            order = EditorGUILayout.IntField(order, GUILayout.Width(40));
            if (order < 0)
            {
                order = 0;
            }
            
            GUILayout.Label("File Name: ");
            _scriptableObjectFileName = EditorGUILayout.TextField(_scriptableObjectFileName, GUILayout.Width(100));

            
            GUILayout.Label("Menu Name: ");
            _scriptableObjectMenuName = EditorGUILayout.TextField(_scriptableObjectMenuName, GUILayout.Width(100));
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"{_baseName}ScriptableObject", GUILayout.Width(_btnWidth * 2)))
            {
                if (string.IsNullOrEmpty(_scriptableObjectFileName) || string.IsNullOrEmpty(_scriptableObjectMenuName))
                {
                    FileNameOrMenuNameNullDialog();
                    return;
                }
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }
                
                MVCCodeGenerator.CreateScriptableObject(_namespace, _baseName, order, _scriptableObjectFileName,_scriptableObjectMenuName, UnityMVCResources.Data.removeComments);
                _scriptableObjectFileName = String.Empty;
                _scriptableObjectMenuName = string.Empty;
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void UnityComponentArea()
        {
            HeaderCreate("Unity Component");
            
            UnityComponentButton();
            //SingleTypesDropdown(ref _loaderSolverAndContainerIntex, _loaderSolverAndContainerTypes);
            GUILayout.Space(7);
        }
        
        private void OnChangedValue()
        {
            UnityMVCResources.SaveAllData();
            _modulePath = UnityMVCResources.Data.modulesRelativePath;
            _assetModulespath = UnityMVCResources.Data.assetModulesRelativePath;
        }

        private void OnCreatedFile()
        {
            UnityMVCResources.LoadData();
            UpdateAllTypes();
            UpdateDependencies();
            _hasApplication = HasApplication();
            AssetDatabase.Refresh();
        }

        public static List<string> GetTypesList(Type objectType, bool addFirstItem, string suffix, string namespaceFilter)
        {
            List<string> objects = new List<string>();
            objects.Add("Select a Class...");
            if (addFirstItem)
            {
                objects.Add("Base");
            }

            List<Type> types = MVCReflectionUtil.GetTypes(objectType, namespaceFilter);
            
            if (types == null || types.Count == 0)
            {
                return objects;
            }

            foreach (Type type in types)
            {
                string str = $"{type.Name}{suffix}";
                objects.Add(str);
            }
            
            return objects;
        }
        private void UpdateAllTypes()
        {
            MVCReflectionUtil.UpdateData();
            
            UpdateTypesList(ref _controllerAndViewTypes, typeof(Controller.Controller),_namespace, suffix: " - View");
            UpdateTypesList(ref  _loaderSolverAndContainerTypes, typeof(Loader), _namespace, suffix: " - Solver - Container");
            UpdateTypesList(ref _controllerTypes, typeof(Controller.Controller), _namespace);
            UpdateTypesList(ref _viewTypes, typeof(View.View), _namespace);
            UpdateTypesList(ref _componentTypes, typeof(MVCComponent), _namespace);
            UpdateTypesList(ref _containerTypes, typeof(Container), _namespace);
            UpdateTypesList(ref _loaderTypes, typeof(Loader), _namespace);
            UpdateTypesList(ref _solverTypes, typeof(Solver), _namespace);
            UpdateTypesList(ref _componentViewTypes, typeof(View.View), _namespace, false);
            UpdateTypesList(ref _unityComponentTypes, typeof(UnityComponent.UnityComponent), _namespace);
            UpdateModules(ref _modules, typeof(View.View));
            ResetAllTypeIndexes();
        }
        private void ResetAllTypeIndexes()
        {
            _controllerAndViewTypeIndex = 0;
            _componentTypeIndex = 0;
            _containerTypeIndex = 0;
            _controllerTypeIndex = 0;
            _loaderTypeIndex = 0;
            _solverTypeIndex = 0;
            _viewTypeIndex = 0;
        }
        private void UpdateTypesList(ref List<string> types, Type type, string namespaceFilter, bool addFirstItem = true, string suffix = "")
        {
            types.Clear();
            types = GetTypesList(type, addFirstItem, suffix, namespaceFilter);
        }
        
        private void UpdateModules(ref List<UnityMVCModuleModel> models, Type type)
        {
            models.Clear();
            UnityMVCModuleData.UpdateModulesList();
            models = UnityMVCModuleData.ProjectModules;
        }
        private void TypesListDropdown(ref int index, List<string> types, float sizeMultiplier = 1, string label = "")
        {
            index = EditorGUILayout.Popup(label, index, types.ToArray(), GUILayout.Width(_btnWidth * sizeMultiplier));
        }
        
        private void ModulesDropdown(ref int index, List<string> types, int multiplier = 2)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref index, types, multiplier, "Modules");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void SingleTypesDropdown(ref int index, List<string> types, int multiplier = 2)
        {
            LabelInheritFrom();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref index, types, multiplier);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void DoubleTypesDropdown(ref int index1, List<string> types1, ref int index2, List<string> types2)
        {
            LabelInheritFrom();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref index1, types1);
            TypesListDropdown(ref index2, types2);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void CustomSingleTypesDropdown(ref int index, List<string> types, string label, int multiplier = 2)
        {
            SimpleLabel(label);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref index, types, multiplier);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void RegionHeader(string header)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(header, GUILayout.Width(_btnWidth*2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
        }

        private void HeaderCreate(string label)
        {
            RegionHeader($"Create {label}:");
        }
        private void RegionFooter(string str)
        {
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(str, GUILayout.Width(_btnWidth*2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void SimpleLabel(string str)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(str, GUILayout.Width(_btnWidth*2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        private void LabelInheritFrom()
        {
            SimpleLabel("That inherits from:");
        }

        private void ViewAndControllerButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}View", GUILayout.Width(_btnWidth)))
            {
                if (_viewTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _viewTypeIndex == 1? null : _viewTypes[_viewTypeIndex];
                MVCCodeGenerator.CreateView(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }

            if (GUILayout.Button($"{_baseName}Controller", GUILayout.Width(_btnWidth)))
            {
                if (_controllerTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _controllerTypeIndex == 1? null : _controllerTypes[_controllerTypeIndex];
                MVCCodeGenerator.CreateController(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ViewAndControllerButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}View / {_baseName}Controller", GUILayout.Width(_btnWidth * 2)))
            {
                if (_controllerAndViewTypeIndex == 0 || _controllerAndViewTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }

                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }
                
                string controller = _controllerAndViewTypeIndex == 1? null : _controllerTypes[_controllerAndViewTypeIndex];
                string view = _controllerAndViewTypeIndex == 1? null : _viewTypes[_controllerAndViewTypeIndex];
                
                MVCCodeGenerator.CreateViewAndController(_namespace, _baseName, UnityMVCResources.Data.removeComments, controller, view);
                
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ComponentButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}MVCComponent", GUILayout.Width(_btnWidth)))
            {
                if (_componentTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _componentTypeIndex == 1? null : _componentTypes[_componentTypeIndex];
                MVCCodeGenerator.CreateComponent(_namespace, _baseName, UnityMVCResources.Data.removeComments, _componentViewTypes[_componentViewIndex],inheritance);
                OnCreatedFile();
            }
            if (GUILayout.Button($"{_baseName}MVCComponentGroup", GUILayout.Width(_btnWidth)))
            {
                if (_componentTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _componentTypeIndex == 1? null : _componentTypes[_componentTypeIndex];
                MVCCodeGenerator.CreateComponentGroup(_namespace, _baseName, UnityMVCResources.Data.removeComments, _componentViewTypes[_componentViewIndex],inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            CustomSingleTypesDropdown(ref _componentViewIndex, _componentViewTypes, "That references:");
        }
        private void LoaderSolverAndContainerButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}Loader/{_baseName}Solver/{_baseName}Container", GUILayout.Width(_btnWidth * 2)))
            {
                if (_loaderSolverAndContainerIntex == 0 || _loaderSolverAndContainerIntex == 0 || _loaderSolverAndContainerIntex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string loader = _loaderSolverAndContainerIntex == 1? null : _loaderTypes[_loaderSolverAndContainerIntex];
                string solver = _loaderSolverAndContainerIntex == 1? null : _solverTypes[_loaderSolverAndContainerIntex];
                string container = _loaderSolverAndContainerIntex == 1? null : _containerTypes[_loaderSolverAndContainerIntex];
                MVCCodeGenerator.CreateContainer(_namespace, _baseName, UnityMVCResources.Data.removeComments, container);
                MVCCodeGenerator.CreateLoader(_namespace, _baseName, UnityMVCResources.Data.removeComments, loader);
                MVCCodeGenerator.CreateSolver(_namespace, _baseName, UnityMVCResources.Data.removeComments, solver);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ContainerButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}Container", GUILayout.Width(_btnWidth * 2)))
            {
                if (_containerTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _containerTypeIndex == 1? null : _containerTypes[_containerTypeIndex];
                
                MVCCodeGenerator.CreateContainer(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void LoaderAndSolverButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}Loader", GUILayout.Width(_btnWidth)))
            {
                if (_loaderTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _loaderTypeIndex == 1? null : _loaderTypes[_loaderTypeIndex];
                MVCCodeGenerator.CreateLoader(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }
            
            if (GUILayout.Button($"{_baseName}Solver", GUILayout.Width(_btnWidth)))
            {
                if (_solverTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_namespace))
                {
                    ShowNameSpaceEmptyDialog();
                    return;
                }

                string inheritance = _solverTypeIndex == 1? null : _solverTypes[_solverTypeIndex];
                MVCCodeGenerator.CreateSolver(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void UnityComponentButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"{_baseName}UnityComponent", GUILayout.Width(_btnWidth * 2)))
            {
                if (_unityComponentIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                
                if (String.IsNullOrEmpty(_baseName))
                {
                    ShowNameEmptyDialog();
                    return;
                }
                string inheritance = _unityComponentIndex == 1? null : _unityComponentTypes[_unityComponentIndex];
                MVCCodeGenerator.CreateUnityComponent(_namespace, _baseName, UnityMVCResources.Data.removeComments, inheritance);
                OnCreatedFile();
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            CustomSingleTypesDropdown(ref _unityComponentIndex, _unityComponentTypes, "That inherits from:");
        }
        
        private void ShowInvalidPathDialog()
        {
            EditorUtility.DisplayDialog("Invalid path", "Please, select a path inside the project to create your files.", "Ok!");
        }
        
        private void ShowNameEmptyDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, add a name to your class or module.", "Ok!");

        }
        
        private void ShowCompanyNameEmptyDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, add a valid Company Name.", "Ok!");

        }

        private void FileNameOrMenuNameNullDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, add File Name or Menu Name", "Ok!"); 
        }

        private void ShowNameSpaceEmptyDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, add a namespace to your class.", "Ok!");
        }
        
        private void ShowModuleAlreadyExistsDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "This module already exists. Please, go to MVC Code Generator tab", "Ok!");
        }

        private void ShowClassSelectionDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, select the reference or inheritance class for your new script.", "Ok!");
        }

        private bool HasApplication()
        {
            List<string> assets = AssetDatabase.FindAssets("Project").ToList();

            List<string> paths = new List<string>();

            for (int i = 0; i < assets.Count; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(assets[i]));
            }
            
            string path = paths.FirstOrDefault(x => x.Contains("/Project.mvc"));

            if (paths == null || path == null || paths.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool PathIsValid(string path)
        {
            return path.StartsWith(Application.dataPath);
        }

        private void HighlightPath(string path)
        {
            string absoluthePath = $"{Application.dataPath}/{path}/Scripts";
                
            if (!Directory.Exists(absoluthePath) && !absoluthePath.Contains("Scripts"))
            {
                Directory.CreateDirectory(absoluthePath);
            }
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            path = $"Assets/{path}/Scripts";

            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
            
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }
    }
}
#pragma warning restore 414

#endif
