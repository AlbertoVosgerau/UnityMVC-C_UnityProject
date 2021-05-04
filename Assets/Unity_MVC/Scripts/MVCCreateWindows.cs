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
            baseName = EditorGUILayout.TextField("Base Name", baseName);
            GUILayout.Space(20);
            GUILayout.BeginVertical();
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
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create View/Controller", GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateViewAndController(baseName);
            }
            if (GUILayout.Button("Create Component", GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateComponent(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create Repository", GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateRepository(baseName);
            }
            
            if (GUILayout.Button("Create Service",GUILayout.Width(btnWidth)))
            {
                MVCCodeGenerator.CreateService(baseName);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}