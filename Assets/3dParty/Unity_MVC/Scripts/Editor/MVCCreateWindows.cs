using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    public class MVCCreateWindows : EditorWindow
    {
        private string _baseName = "";
        private float _btnWidth = 150;
        private UnityMVCData _data;
        private int _typeIndex = 0;
        private List<string> _types = new List<string>();

        [MenuItem("Unity MVC/Open Creation Window")]
        private static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC Generator");
            window.Show();
        }

        private void OnEnable()
        {
            // TODO: Add inheritance to system
            //CheckTypes();
        }

        void OnGUI()
        {
            // TODO: Add inheritance to system
            //UpdateTypesList();
            GUILayout.Label("Create MVC Script", EditorStyles.boldLabel);
            GUILayout.Space(20);
            SetMVCData();
            EditorGUI.BeginChangeCheck();
            _data.editorData.scriptsFolder = EditorGUILayout.TextField("Create in Assets/",_data.editorData.scriptsFolder);
            if (EditorGUI.EndChangeCheck())
            {
                _data.SaveData(MVCCodeGenerator.GetMVCDataPath());
            }
            GUILayout.Space(10);
            _baseName = EditorGUILayout.TextField("Base File Name", _baseName);
            GUILayout.BeginVertical();
            GUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            _data.editorData.removeComments = GUILayout.Toggle(_data.editorData.removeComments, "Remove comments from generated code");
            if (EditorGUI.EndChangeCheck())
            {
                _data.SaveData(MVCCodeGenerator.GetMVCDataPath());
            }
            GUILayout.Space(20);
            ViewAndController();
            GUILayout.Space(20);
            Component();
            GUILayout.Space(20);
            LoaderSolverAndContainer();
            GUILayout.EndVertical();
        }

        private void CheckTypes()
        {
            _types.Clear();
            _types.Add("Base");
            var viewTypes = GetTypesList(typeof(Controller.Controller)).ToArray();
            
            foreach (string viewType in viewTypes)
            {
                _types.Add(viewType);
            }
        }
        
        public static List<string> GetTypesList(Type objectType)
        {
            List<string> objects = new List<string>();
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

        private void UpdateTypesList()
        {
            _typeIndex = EditorGUILayout.Popup(_typeIndex, _types.ToArray());
        }

        private void SetMVCData()
        {
            _data = MVCCodeGenerator.GetMVCData();
            _data.LoadData(MVCCodeGenerator.GetMVCDataPath());
        }

        private void ViewAndController()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View / Controller", GUILayout.Width(_btnWidth*2)))
            {
                MVCCodeGenerator.CreateViewAndController(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateView(_baseName, _data.editorData.removeComments);
            }
            if (GUILayout.Button("Create Controller", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateController(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void Component()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Component", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateComponent(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void LoaderSolverAndContainer()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader / Solver / Container", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments);
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments);
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Container", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateLoader(_baseName, _data.editorData.removeComments);
            }
            
            if (GUILayout.Button("Create Solver",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateSolver(_baseName, _data.editorData.removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#if UNITY_EDITOR
#endif
