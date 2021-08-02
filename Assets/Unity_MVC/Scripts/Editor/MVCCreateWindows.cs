using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityMVC.Component;
using UnityMVC.Model;

namespace UnityMVC
{
    public class MVCCreateWindows : EditorWindow
    {
        private string _baseName = "";
        private float _btnWidth = 150;
        private Vector2 _scrollPosition = new Vector2(0,0);
        private int _currentTab = 0;
        private string[] _tabs = new[] {"Controllers and Views", "Components", "Models"};
        private int _currentPath = 0;
        private List<string> _dataPaths;
        
        private UnityMVCData _data;
        
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
        
        private List<string> _componentViewTypes = new List<string>();
        private int _componentViewIndex;
        private bool _hasApplication = false;

        [MenuItem("Unity MVC/Open Creation Window", priority = 0)]
        private static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC Generator");
            window.Show();
        }

        private void OnEnable()
        {
            _hasApplication = HasApplication();
            //SolveDatapaths();
            SetMVCData();
            UpdateAllTypes();
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
            Header();
            GUILayout.Space(20);
            
            CreateAssetAtArea();
            GUILayout.Space(10);
            
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
                Tabs();
            }

            //Footer();
            GUILayout.EndVertical();
        }

        private static void Header()
        {
            GUILayout.Label("Create MVC Script", EditorStyles.boldLabel);
        }
        
        private void CreateApplicationArea()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
                        
            if (GUILayout.Button($"Create MVCApplication", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateApplication(_baseName);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void Tabs()
        {
            _currentTab = GUILayout.Toolbar(_currentTab, _tabs);

            GUILayout.Space(20);
            switch (_currentTab)
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
            }
        }
        private void Footer()
        {
            RegionFooter("Created by Alberto Vosgerau Neto\n\nFeel free to change this in any way you wish ;)\nQuestions and suggestions:\n\nalberto@argames.com.br\n\nWill add documentation link soon, I promise!\n\nHappy coding!");
        }

        
        private void CreateAssetAtArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            _data.editorData.scriptsFolder = EditorGUILayout.TextField("Create in Assets/", _data.editorData.scriptsFolder,GUILayout.Width(_btnWidth * 2));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
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
            _data.editorData.removeComments = GUILayout.Toggle(_data.editorData.removeComments,
                " Remove comments from generated code", GUILayout.Width(_btnWidth * 2));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ViewAndControllerArea()
        {
            RegionHeader("View and Controller");

            ViewAndControllerButton();
            SingleTypesDropdown(ref _controllerAndViewTypeIndex, _controllerAndViewTypes);
            GUILayout.Space(10);

            ViewAndControllerButtons();
            DoubleTypesDropdown(ref _viewTypeIndex, _viewTypes, ref _controllerTypeIndex, _controllerTypes);
        }
        private void ComponentArea()
        {
            RegionHeader("Component");
            
            ComponentButton();
            CustomSingleTypesDropdown(ref _componentTypeIndex, _componentTypes, "and inherits from:");
        }
        private void LoaderSolverAndContainerArea()
        {
            RegionHeader("Model");
            
            LoaderSolverAndContainerButton();
            SingleTypesDropdown(ref _loaderSolverAndContainerIntex, _loaderSolverAndContainerTypes);
            GUILayout.Space(7);

            ContainerButton();
            SingleTypesDropdown(ref _containerTypeIndex, _containerTypes);
            GUILayout.Space(7);
            
            LoaderAndSolverButtons();
            DoubleTypesDropdown(ref  _loaderTypeIndex, _loaderTypes, ref _solverTypeIndex, _solverTypes);
        }


        private void SetMVCData()
        {
            _data = MVCCodeGenerator.GetMVCData();
            _data.LoadData(MVCCodeGenerator.GetMVCDataPath());
        }

        private void OnChangedValue()
        {
            _data.SaveData(MVCCodeGenerator.GetMVCDataPath());
        }

        private void OnCreatedFile()
        {
            UpdateAllTypes();
            _hasApplication = HasApplication();
        }

        public static List<string> GetTypesList(Type objectType, bool addFirstItem, string suffix)
        {
            List<string> objects = new List<string>();
            objects.Add("Select a Class...");
            if (addFirstItem)
            {
                objects.Add("Base");
            }
            foreach (Type type in Assembly.GetAssembly(objectType).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(objectType) && !myType.Name.Contains("Template")))
            {
                string str = $"{type.Name}{suffix}";
                objects.Add(str);
            }
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);

                foreach (var attribute in assembly.GetCustomAttributesData())
                {
                    Console.WriteLine(attribute);
                }
                Console.WriteLine();
            }
            return objects;
        }
        private void UpdateAllTypes()
        {
            UpdateTypesList(ref _controllerAndViewTypes, typeof(Controller.Controller), suffix: " - View");
            UpdateTypesList(ref  _loaderSolverAndContainerTypes, typeof(Loader), suffix: " - Solver - Container");
            UpdateTypesList(ref _controllerTypes, typeof(Controller.Controller));
            UpdateTypesList(ref _viewTypes, typeof(View.View));
            UpdateTypesList(ref _componentTypes, typeof(MVCComponent));
            UpdateTypesList(ref _containerTypes, typeof(Container));
            UpdateTypesList(ref _loaderTypes, typeof(Loader));
            UpdateTypesList(ref _solverTypes, typeof(Solver));
            UpdateTypesList(ref _componentViewTypes, typeof(View.View), false);
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
        private void UpdateTypesList(ref List<string> types, Type type, bool addFirstItem = true, string suffix = "")
        {
            types.Clear();
            types = GetTypesList(type, addFirstItem, suffix);
        }
        private void TypesListDropdown(ref int index, List<string> types, float sizeMultiplier = 1)
        {
            index = EditorGUILayout.Popup(index, types.ToArray(), GUILayout.Width(_btnWidth * sizeMultiplier));
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
            
            if (GUILayout.Button($"Create {_baseName}View", GUILayout.Width(_btnWidth)))
            {
                if (_viewTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _viewTypeIndex == 1? null : _viewTypes[_viewTypeIndex];
                MVCCodeGenerator.CreateView(_baseName, _data.editorData.removeComments, inheritance);
                OnCreatedFile();
            }

            if (GUILayout.Button($"Create {_baseName}Controller", GUILayout.Width(_btnWidth)))
            {
                if (_controllerTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _controllerTypeIndex == 1? null : _controllerTypes[_controllerTypeIndex];
                MVCCodeGenerator.CreateController(_baseName, _data.editorData.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ViewAndControllerButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"Create {_baseName}View / {_baseName}Controller", GUILayout.Width(_btnWidth * 2)))
            {
                if (_controllerAndViewTypeIndex == 0 || _controllerAndViewTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string controller = _controllerAndViewTypeIndex == 1? null : _controllerTypes[_controllerAndViewTypeIndex];
                string view = _controllerAndViewTypeIndex == 1? null : _viewTypes[_controllerAndViewTypeIndex];
                
                MVCCodeGenerator.CreateViewAndController(_baseName, _data.editorData.removeComments, controller, view);
                
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ComponentButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"Create {_baseName}Component", GUILayout.Width(_btnWidth * 2)))
            {
                if (_componentTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _componentTypeIndex == 1? null : _componentTypes[_componentTypeIndex];
                MVCCodeGenerator.CreateComponent(_baseName, _data.editorData.removeComments, _componentViewTypes[_componentViewIndex],inheritance);
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
            
            if (GUILayout.Button($"Create {_baseName}Loader/{_baseName}Solver/{_baseName}Container", GUILayout.Width(_btnWidth * 2)))
            {
                if (_loaderSolverAndContainerIntex == 0 || _loaderSolverAndContainerIntex == 0 || _loaderSolverAndContainerIntex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string loader = _loaderSolverAndContainerIntex == 1? null : _loaderTypes[_loaderSolverAndContainerIntex];
                string solver = _loaderSolverAndContainerIntex == 1? null : _solverTypes[_loaderSolverAndContainerIntex];
                string container = _loaderSolverAndContainerIntex == 1? null : _containerTypes[_loaderSolverAndContainerIntex];
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments, container);
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments, loader);
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments, solver);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void ContainerButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"Create {_baseName}Container", GUILayout.Width(_btnWidth * 2)))
            {
                if (_containerTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _containerTypeIndex == 1? null : _containerTypes[_containerTypeIndex];
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void LoaderAndSolverButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"Create {_baseName}Loader", GUILayout.Width(_btnWidth)))
            {
                if (_loaderTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _loaderTypeIndex == 1? null : _loaderTypes[_loaderTypeIndex];
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments, inheritance);
                OnCreatedFile();
            }
            
            if (GUILayout.Button($"Create {_baseName}Solver", GUILayout.Width(_btnWidth)))
            {
                if (_solverTypeIndex == 0)
                {
                    ShowClassSelectionDialog();
                    return;
                }
                string inheritance = _solverTypeIndex == 1? null : _solverTypes[_solverTypeIndex];
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments, inheritance);
                OnCreatedFile();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
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
            
            string path = paths.FirstOrDefault(x => !x.Contains("Unity_MVC/") && !x.Contains("Unity_MVC\\"));

            if (paths == null || path == null)
            {
                return false;
            }
            return true;
        }
    }
}
#if UNITY_EDITOR
#endif
