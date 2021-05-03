using UnityEditor;
using UnityEngine;

namespace UnityMVC
{
    public class MVCCreateWindows : EditorWindow
    {
        string baseName = "";
    
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
            if (GUILayout.Button("Create View/Controller"))
            {
                MVCCodeGenerator.CreateViewAndController(baseName);
            }
            if (GUILayout.Button("Create Component  "))
            {
                MVCCodeGenerator.CreateComponent(baseName);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(7);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Repository  "))
            {
                MVCCodeGenerator.CreateRepository(baseName);
            }
            
            if (GUILayout.Button("Create Service  "))
            {
                MVCCodeGenerator.CreateService(baseName);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}