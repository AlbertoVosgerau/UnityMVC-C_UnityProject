using UnityMVC.UnityComponent;

namespace SecondNamespace
{
    public class InheritedUnityComponent : SecondUnityComponent
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