using UnityEngine;

namespace UnityMVC
{
    public class UnityMVCData : ScriptableObject
    {
        public string Root => _root;
        [Header("Root project folder inside Assets/")]
        [SerializeField] private string _root;
        
        public string ControllersPath => $"{_root}/{_controllersPath}";
        [Header("Controllers will be created at Assets/")]
        [SerializeField] private string _controllersPath;
        
        public string ViewsPath => $"{_root}/{_viewsPath}";
        [Header("Views will be created at Assets/")]
        [SerializeField] private string _viewsPath;
        
        public string ComponentsPath => $"{_root}/{_componentsPath}";
        [Header("Components will be created at Assets/")]
        [SerializeField] private string _componentsPath;
        
        public string RepositoriesPath => $"{_root}/{_repositoriesPath}";
        [Header("Repositories will be created at Assets/")]
        [SerializeField] private string _repositoriesPath;
        
        public string ServicesPath => $"{_root}/{_servicesPath}";
        [Header("Services will be created at Assets/")]
        [SerializeField] private string _servicesPath;
    }
}