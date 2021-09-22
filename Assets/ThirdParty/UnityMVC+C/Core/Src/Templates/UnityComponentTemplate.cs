using UnityMVC.UnityComponent;

/*<NAMESPACE>*/
    public class UnityComponentTemplate : UnityComponent
    {
        private void SolveDependencies()
        {
        }

        private void RegisterEvents()
        {
            // otherObject.EventName += MyMethod;
        }

        private void UnregisterEvents()
        {
            // otherObject.EventName -= MyMethod;
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
/*}*/