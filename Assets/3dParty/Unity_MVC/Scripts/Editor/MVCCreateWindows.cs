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
        private UnityMVCData _data;
        private List<string> _controllerTypes = new List<string>();
        private int _controllerTypeIndex;
        private List<string> _viewTypes = new List<string>();
        private int _viewTypeIndex;
        private List<string> _componentTypes = new List<string>();
        private int _componentTypeIndex;
        private List<string> _containerrTypes = new List<string>();
        private int _containerTypeIndex;
        private List<string> _loaderTypes = new List<string>();
        private int _loaderTypeIndex;
        private List<string> _solverTypes = new List<string>();
        private int _solverTypeIndex;

        [MenuItem("Unity MVC/Open Creation Window")]
        private static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC Generator");
            window.Show();
        }

        private void OnEnable()
        {
            UpdateAllTypes();
        }

        void OnGUI()
        {
            GUILayout.Label("Create MVC Script", EditorStyles.boldLabel);
            GUILayout.Space(20);
            SetMVCData();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            _data.editorData.scriptsFolder = EditorGUILayout.TextField("Create in Assets/",_data.editorData.scriptsFolder, GUILayout.Width(_btnWidth*2));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _baseName = EditorGUILayout.TextField("Base File Name", _baseName, GUILayout.Width(_btnWidth*2));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            _data.editorData.removeComments = GUILayout.Toggle(_data.editorData.removeComments, "Remove comments from generated code", GUILayout.Width(_btnWidth*2));
            if (EditorGUI.EndChangeCheck())
            {
                OnChangedValue();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);
            ViewAndController();
            GUILayout.Space(20);
            Component();
            GUILayout.Space(20);
            LoaderSolverAndContainer();
            GUILayout.EndVertical();
            Footer();
        }

        private void Footer()
        {
            RegionFooter("Created by Alberto Vosgerau Neto\n\nFeel free to change this in any way you wish.\nQuestions and suggestions:\nalberto@argames.com.br\n\nWill add documentation link soon, I promise!\n\nHappy coding!");
        }

        private void OnChangedValue()
        {
            _data.SaveData(MVCCodeGenerator.GetMVCDataPath());
        }

        private void OnCreatedFile()
        {
            UpdateAllTypes();
        }

        public static List<string> GetTypesList(Type objectType)
        {
            List<string> objects = new List<string>();
            objects.Add("Base");
            foreach (Type type in Assembly.GetAssembly(objectType).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(objectType) && !myType.Name.Contains("Template")))
            {
                objects.Add(type.Name);
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
            UpdateTypesList(ref _controllerTypes, typeof(Controller.Controller));
            UpdateTypesList(ref _viewTypes, typeof(View.View));
            UpdateTypesList(ref _componentTypes, typeof(MVCComponent));
            UpdateTypesList(ref _containerrTypes, typeof(Container));
            UpdateTypesList(ref _loaderTypes, typeof(Loader));
            UpdateTypesList(ref _solverTypes, typeof(Solver));
            ResetAllTypeIndexes();
        }

        private void ResetAllTypeIndexes()
        {
            _componentTypeIndex = 0;
            _containerTypeIndex = 0;
            _controllerTypeIndex = 0;
            _loaderTypeIndex = 0;
            _solverTypeIndex = 0;
            _viewTypeIndex = 0;
        }
        private void UpdateTypesList(ref List<string> types, Type type)
        {
            types.Clear();
            types = GetTypesList(type);
        }

        private void TypesListDropdown(ref int index, List<string> types)
        {
            index = EditorGUILayout.Popup(index, types.ToArray(), GUILayout.Width(_btnWidth));
        }

        private void SetMVCData()
        {
            _data = MVCCodeGenerator.GetMVCData();
            _data.LoadData(MVCCodeGenerator.GetMVCDataPath());
        }

        private void RegionHeader(string header)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(header, GUILayout.Width(_btnWidth*2));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
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

        private void ViewAndController()
        {
            RegionHeader("View and Controller");

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View / Controller", GUILayout.Width(_btnWidth*2)))
            {
                MVCCodeGenerator.CreateViewAndController(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TypesListDropdown(ref _viewTypeIndex, _viewTypes);
            TypesListDropdown(ref _controllerTypeIndex, _controllerTypes);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateView(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            if (GUILayout.Button("Create Controller", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateController(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void Component()
        {
            RegionHeader("Component");
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Component", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateComponent(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void LoaderSolverAndContainer()
        {
            RegionHeader("Model");
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader / Solver / Container", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments);
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments);
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Container", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            
            if (GUILayout.Button("Create Solver",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments);
                OnCreatedFile();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#if UNITY_EDITOR
#endif
