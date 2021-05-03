using UnityEngine;

namespace UnityMVC
{
    public class UnityMVCData : ScriptableObject
    {
        public string ControllersPath => _controllersPath;
        [Header("Controllers will be created at Assets/")]
        [SerializeField] private string _controllersPath;
        
        public string ViewsPath => _viewsPath;
        [Header("Views will be created at Assets/")]
        [SerializeField] private string _viewsPath;
        
        public string RepositoriesPath => _repositoriesPath;
        [Header("Repositories will be created at Assets/")]
        [SerializeField] private string _repositoriesPath;
        
        public string ServicesPath => _servicesPath;
        [Header("Services will be created at Assets/")]
        [SerializeField] private string _servicesPath;
    }
}