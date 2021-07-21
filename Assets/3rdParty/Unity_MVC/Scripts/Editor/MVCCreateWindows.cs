using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    #if UNITY_EDITOR
    public class MVCCreateWindows : EditorWindow
    {
        private string _baseName = "";
        private float _btnWidth = 150;
        private bool _removeComments = false;

        [MenuItem("Unity MVC/Open Creation Window")]
        static void Init()
        {
            MVCCreateWindows window = (MVCCreateWindows)GetWindow(typeof(MVCCreateWindows));
            window.titleContent = new GUIContent("MVC Generator");
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Create MVC Script", EditorStyles.boldLabel);
            GUILayout.Space(20);
            UnityMVCData data = MVCCodeGenerator.GetMVCData();
            data.scriptsFolder = EditorGUILayout.TextField("Create in Assets/",data.scriptsFolder);
            GUILayout.Space(10);
            _baseName = EditorGUILayout.TextField("Base File Name", _baseName);
            GUILayout.BeginVertical();
            GUILayout.Space(5);
            _removeComments = GUILayout.Toggle(_removeComments, "Remove comments from generated code");
            GUILayout.Space(20);
            ViewAndController();
            GUILayout.Space(20);
            Component();
            GUILayout.Space(20);
            LoaderSolverAndContainer();
            GUILayout.EndVertical();
        }

        private void ViewAndController()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View / Controller", GUILayout.Width(_btnWidth*2)))
            {
                MVCCodeGenerator.CreateViewAndController(_baseName, _removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateView(_baseName, _removeComments);
            }
            if (GUILayout.Button("Create Controller", GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateController(_baseName, _removeComments);
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
                MVCCodeGenerator.CreateComponent(_baseName, _removeComments);
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
                MVCCodeGenerator.CreateContainer(_baseName, _removeComments);
                MVCCodeGenerator.CreateLoader(_baseName, _removeComments);
                MVCCodeGenerator.CreateSolver(_baseName, _removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Container", GUILayout.Width(_btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(_baseName, _removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateLoader(_baseName, _removeComments);
            }
            
            if (GUILayout.Button("Create Solver",GUILayout.Width(_btnWidth)))
            {
                MVCCodeGenerator.CreateSolver(_baseName, _removeComments);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
    #endif
}