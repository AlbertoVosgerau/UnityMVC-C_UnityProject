using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityMVC.Component;
using UnityMVC.Model;

namespace UnityMVC.Editor
{
#pragma warning disable 414
    public class MVCCreateWindows : EditorWindow
    {
        private int _mainTabIntex = 0;
        private string[] _mainTabs = new[] {"Module Wizard", "MVC+C Code Generator", "Inspector"};

        private string _newModuleName;
        private string _newNamespace;

        private string _modulePath;
        
        private string _namespacePrefix = "";
        private string _namespace = "";
        private string _baseName = "";
        private string _projectName = "";
        private float _btnWidth = 220;
        private Vector2 _scrollPosition = new Vector2(0,0);
        private int _currentMVCTab = 0;
        private string[] _MVCtabs = new[] {"Controllers/Views", "MVCComponents", "Models", "UnityComponent"};
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

        private MVCInspectorData _controllersDependencies;
        private MVCInspectorData _viewDependencies;
        private MVCInspectorData _mvcComponentDependencies;
        private MVCInspectorData _unityComponentDependencies;
        

        private List<string> _componentViewTypes = new List<string>();
        private int _componentViewIndex;
        private bool _hasApplication = false;

        [MenuItem("Unity MVC+C/Open Creation Window", priority = 0)]
        private static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC+C Generator");
            window.Show();
        }

        private void OnEnable()
        {
            _hasApplication = HasApplication();
            //SolveDatapaths();
            UnityMVCResources.LoadData();
            _modulePath = UnityMVCResources.Data.modulesRelativePath;
            UnityMVCModuleData.GetAllModules();
            UpdateAllTypes();
            UpdateDependencies();
        }

        private void UpdateDependencies()
        {
            _controllersDependencies = MVCInspector.GetDependencies(typeof(Controller.Controller));
            _viewDependencies = MVCInspector.GetDependencies(typeof(View.View));
            _mvcComponentDependencies = MVCInspector.GetDependencies(typeof(MVCComponent));
            _unityComponentDependencies = MVCInspector.GetDependencies(typeof(UnityComponent.UnityComponent));
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

        private static void Header()
        {
            GUILayout.Label("Create MVC+C Script", EditorStyles.boldLabel);
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
            
            _projectName = EditorGUILayout.TextField("Base File Name", _projectName, GUILayout.Width(_btnWidth * 2));     
            
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
            

            if (GUILayout.Button($"Create MVCApplication", GUILayout.Width(_btnWidth * 2)))
            {
                if (string.IsNullOrWhiteSpace(_projectName))
                {
                    ShowNameEmptyDialog();
                    return;
                }
                
                MVCCodeGenerator.CreateApplication(_projectName);
                OnCreatedFile();
            }

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
            }
        }

        private void MVCTabs()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            GUIStyle style = new GUIStyle(GUI.skin.button);;
            style.fontSize = 2;

            _currentMVCTab = GUILayout.Toolbar(_currentMVCTab, _MVCtabs, style, GUILayout.Width(_btnWidth *2) );
            
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
                    LoaderSolverAndContainerArea();
                    break;
                case 3:
                    UnityComponentArea();
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
            RemoveCommentsFromGeneratedCodeArea();
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

        private void MVCAreaCodeGeneratorArea()
        {
            Header();
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
                RemoveCommentsFromGeneratedCodeArea();
                GUILayout.Space(20);
                MVCTabs();
            }
            
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
            GUILayout.Label($"Still a work in progress.", GUILayout.Width(_btnWidth * 2));
            GUILayout.Space(20);
            
            DependencyFeedback(_controllersDependencies, MessageType.Info);
            DependencyFeedback(_mvcComponentDependencies, MessageType.Info);
            DependencyFeedback(_viewDependencies, MessageType.Warning);
            DependencyFeedback(_unityComponentDependencies, MessageType.Warning);
        }

        private void DependencyFeedback(MVCInspectorData data, MessageType messageType)
        {
            foreach (var dependency in data.results)
            {
                GUILayout.Label($"{dependency.type.Name} depends on:", GUILayout.Width(_btnWidth * 2));
                foreach (var value in dependency.dependenciesRoot)
                {
                    EditorGUILayout.HelpBox($"{value.FieldType} on variable {value.Name}", messageType);
                }
                GUILayout.Space(20);
            }
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
            NamespacesDropdown(ref _moduleIndex, names);
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
            UnityMVCResources.Data.removeComments = GUILayout.Toggle(UnityMVCResources.Data.removeComments, " Remove comments from generated code", GUILayout.Width(_btnWidth * 2));
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
            HeaderCreate();

            ViewAndControllerButton();
            SingleTypesDropdown(ref _controllerAndViewTypeIndex, _controllerAndViewTypes);
            GUILayout.Space(10);

            ViewAndControllerButtons();
            DoubleTypesDropdown(ref _viewTypeIndex, _viewTypes, ref _controllerTypeIndex, _controllerTypes);
        }
        private void ComponentArea()
        {
            HeaderCreate();
            
            ComponentButton();
            CustomSingleTypesDropdown(ref _componentTypeIndex, _componentTypes, "and inherits from:");
        }
        private void LoaderSolverAndContainerArea()
        {
            HeaderCreate();
            
            LoaderSolverAndContainerButton();
            SingleTypesDropdown(ref _loaderSolverAndContainerIntex, _loaderSolverAndContainerTypes);
            GUILayout.Space(7);
            
            HeaderCreate();
            
            ContainerButton();
            SingleTypesDropdown(ref _containerTypeIndex, _containerTypes);
            GUILayout.Space(7);
            
            HeaderCreate();
            
            LoaderAndSolverButtons();
            DoubleTypesDropdown(ref  _loaderTypeIndex, _loaderTypes, ref _solverTypeIndex, _solverTypes);
        }
        
        private void UnityComponentArea()
        {
            HeaderCreate();
            
            UnityComponentButton();
            //SingleTypesDropdown(ref _loaderSolverAndContainerIntex, _loaderSolverAndContainerTypes);
            GUILayout.Space(7);
        }
        
        private void OnChangedValue()
        {
            UnityMVCResources.SaveAllData();
        }

        private void OnCreatedFile()
        {
            UnityMVCResources.LoadData();
            UpdateAllTypes();
            UpdateDependencies();
            _hasApplication = HasApplication();
        }

        public static List<string> GetTypesList(Type objectType, bool addFirstItem, string suffix, string namespaceFilter)
        {
            List<string> objects = new List<string>();
            objects.Add("Select a Class...");
            if (addFirstItem)
            {
                objects.Add("Base");
            }

            List<Type> types = Assembly.GetAssembly(objectType).GetTypes().ToList();
            
            if (types == null || types.Count == 0)
            {
                return objects;
            }

            List<Type> filteredTypes = types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                x.IsSubclassOf(objectType) &&
                !x.Name.Contains("Template") &&
                x.Namespace.Contains(namespaceFilter)).ToList();

            foreach (Type type in filteredTypes)
            {
                string str = $"{type.Name}{suffix}";
                objects.Add(str);
            }
            
            return objects;
        }
        
        public static List<string> GetTNamespacesList(Type objectType)
        {
            List<string> objects = new List<string>();
            
            List<Type> types = Assembly.GetAssembly(objectType).GetTypes().ToList();
            
            if (types == null || types.Count == 0)
            {
                return objects;
            }

            List<Type> filteredTypes = types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                x.IsSubclassOf(objectType) &&
                !x.Name.Contains("Template")).ToList();


            foreach (Type type in filteredTypes)
            {
                string str = $"{type.Namespace}";
                if (objects.Contains(str))
                {
                    continue;
                }
                objects.Add(str);
            }
            return objects;
        }
        
        private void UpdateAllTypes()
        {
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
            models = UnityMVCModuleData.GetAllModules();
        }
        private void TypesListDropdown(ref int index, List<string> types, float sizeMultiplier = 1, string label = "")
        {
            index = EditorGUILayout.Popup(label, index, types.ToArray(), GUILayout.Width(_btnWidth * sizeMultiplier));
        }
        
        private void NamespacesDropdown(ref int index, List<string> types, int multiplier = 2)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref index, types, multiplier, "Namespace");
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

        private void HeaderCreate()
        {
            RegionHeader("Create:");
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
            
            CustomSingleTypesDropdown(ref _unityComponentIndex, _unityComponentTypes, "That references:");
        }
        
        private void ShowInvalidPathDialog()
        {
            EditorUtility.DisplayDialog("Invalid path", "Please, select a path inside the project to create your files.", "Ok!");
        }
        
        private void ShowNameEmptyDialog()
        {
            EditorUtility.DisplayDialog("Invalid operation", "Please, add a name to your class or module.", "Ok!");

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
            List<string> assets = AssetDatabase.FindAssets("MVCApplication").ToList();

            List<string> paths = new List<string>();

            for (int i = 0; i < assets.Count; i++)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(assets[i]));
            }
            
            string path = paths.FirstOrDefault(x => !x.Contains("/MVCApplication.cs") && !x.Contains("MVCApplicationTemplate.cs"));

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
#if UNITY_EDITOR
#endif
