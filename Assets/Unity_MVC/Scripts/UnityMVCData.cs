using System;
using System.IO;
using UnityEngine;

namespace UnityMVC
{
    
    public class UnityMVCData : ScriptableObject
    {
        public UnityMVCEditorData editorData = new UnityMVCEditorData();
        private readonly string _fileName = "MVCData.mvc";

        public void LoadData(string path)
        {
            string fullPath = GetFullPath(path);
            if (!File.Exists(fullPath))
            {
                SaveData(path);
                return;
            }

            string data = File.ReadAllText(fullPath);
            UnityMVCEditorData newData = JsonUtility.FromJson<UnityMVCEditorData>(data);
            editorData.scriptsFolder = newData.scriptsFolder;
            editorData.removeComments = newData.removeComments;
        }

        public void SaveData(string path)
        {
            string fullPath = GetFullPath(path);
            string serializedData = JsonUtility.ToJson(editorData);
            StreamWriter file = File.CreateText(fullPath);
            file.Write(serializedData);
            file.Close();
        }

        private string GetFullPath(string path)
        {
            string folderPath = path;
            string fullPath = $"{folderPath}/{_fileName}";
            return fullPath;
        }
    }                   
}

[Serializable]
public class UnityMVCEditorData
{
    public string scriptsFolder;
    public bool removeComments;
}