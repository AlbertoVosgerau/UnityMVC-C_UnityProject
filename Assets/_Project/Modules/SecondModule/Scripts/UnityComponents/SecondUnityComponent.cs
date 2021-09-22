using UnityMVC.UnityComponent;

namespace SecondNamespace
{
    public class SecondUnityComponent : UnityComponent
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