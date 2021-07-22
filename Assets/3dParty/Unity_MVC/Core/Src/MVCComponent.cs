using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC.Component
{
    public abstract class MVCComponent : MonoBehaviour
    {
        public string View => _baseView.gameObject.name;
        protected View.View _baseView;
        public UnityEngine.Component Owner => _owner;
        protected UnityEngine.Component _owner;
        
        [SerializeField] private List<UnityEngine.Component> _unityComponents;
        public void SetView(View.View view)
        {
            _baseView = view;
            OnViewWasSet(_baseView);
        }

        protected virtual void OnViewWasSet(View.View view)
        {
            
        }
        public virtual void SetOwner(UnityEngine.Component owner)
        {
            _owner = owner;
        }
        
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
        protected abstract void SolveDependencies();

        protected abstract void InternalStart();
        protected abstract void InternalOnDestroy();

        private void Awake()
        {
            SolveDependencies();
            AwakeMVC();
        }
        private void Start()
        {
            InternalStart();
            StartCoroutine(LateStartRoutine());
            StartMVC();
        }
        private void Update()
        {
            UpdateMVC();
        }
        private void OnEnable()
        {
            OnEnableMVC();
        }
        private void OnDisable()
        {
            OnDisableMVC();
        }
        private void OnDestroy()
        {
            OnDestroyMVC();
            InternalOnDestroy();
        }

        protected virtual void AwakeMVC() {}
        protected virtual void StartMVC() {}
        protected virtual void UpdateMVC() {}
        protected virtual void LateStartMVC() {}
        protected virtual void OnEnableMVC() {}
        protected virtual void OnDisableMVC() {}
        protected virtual void OnDestroyMVC() {}

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStartMVC();
        }
        
        public void RegisterComponent(UnityEngine.Component component)
        {
            _unityComponents.Add(component);
        }
        public void UnregisterComponent(UnityEngine.Component component)
        {
            _unityComponents.Remove(component);
        }
        public T GetUnityComponentFromMVC<T>(bool addToStoredComponentsList = false) where T : UnityEngine.Component
        {
            T component = _unityComponents.FirstOrDefault(x => x.GetType() == typeof(T)) as T;

            if (component == null)
            {
                component = GetComponent<T>();
            }
            
            if (addToStoredComponentsList && component != null)
            {
                if (!_unityComponents.Contains(component))
                {
                    _unityComponents.Add(component);
                }
            }
            return component;
        }
        public List<T> GetUnityComponentsFromMVC<T>(bool addToStoredComponentsList = false) where T : UnityEngine.Component
        {
            List<T> components = _unityComponents.Where(x => x.GetType() == typeof(T)) as List<T>;

            if (components.Count == 0)
            {
                components = GetComponents<T>().ToList();
            }

            if (addToStoredComponentsList && components.Count > 0)
            {
                foreach (T component in components)
                {
                    if (!_unityComponents.Contains(component))
                    {
                        _unityComponents.Add(component);
                    }
                }
            }
            return components;
        }
    }
}