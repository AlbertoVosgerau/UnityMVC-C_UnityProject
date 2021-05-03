using UnityEngine;

namespace UnityMVC
{
    public class UnityMVCData : ScriptableObject
    {
        public string ScriptsFolder => _scriptsFolder;
        [Header("Root project folder inside Assets/")]
        [SerializeField] private string _scriptsFolder;
    }
}