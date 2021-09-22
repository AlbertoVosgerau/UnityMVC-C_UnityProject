using UnityMVC.UnityComponent;

namespace FuirsNamespace
{
    public class FirstUnityComponent : UnityComponent
    {
        private void SolveDependencies()
        {
        }

        private void RegisterEvents()
        {
        }

        private void UnregisterEvents()
        {
        }

        private void Awake()
        {
            SolveDependencies();
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
    }
}