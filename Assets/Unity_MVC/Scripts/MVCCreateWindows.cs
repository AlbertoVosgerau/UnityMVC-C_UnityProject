using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    public class MVCCreateWindows : EditorWindow
    {
        string baseName = "";
        private float btnWidth = 150;

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
            baseName = EditorGUILayout.TextField("Base File Name", baseName);
            GUILayout.Space(20);
            GUILayout.BeginVertical();
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
            if (GUILayout.Button("Create View / Controller", GUILayout.Width(btnWidth*2)))
            {
                MVCCodeGenerator.CreateViewAndController(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View", GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateView(baseName);
            }
            if (GUILayout.Button("Create Controller", GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateController(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void Component()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Component", GUILayout.Width(btnWidth * 2)))
            {
                MVCCodeGenerator.CreateComponent(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void LoaderSolverAndContainer()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader /Solver / Container", GUILayout.Width(btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(baseName);
                MVCCodeGenerator.CreateLoader(baseName);
                MVCCodeGenerator.CreateSolver(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Container", GUILayout.Width(btnWidth * 2)))
            {
                MVCCodeGenerator.CreateContainer(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Loader",GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateLoader(baseName);
            }
            
            if (GUILayout.Button("Create Solver",GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateSolver(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}