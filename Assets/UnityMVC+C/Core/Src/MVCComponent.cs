using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC.Component
{
    public abstract class MVCComponent : MonoBehaviour
    {
        public View.View OwnerView => BaseOwnerView;
        protected View.View BaseOwnerView;
        public UnityEngine.Component Owner => _owner;
        protected UnityEngine.Component _owner;
        
        [SerializeField] private List<UnityEngine.Component> _unityComponents;
        public abstract Type GetViewType();
        public abstract bool IsActive();
        public abstract void SetView(View.View view);
        protected virtual void OnViewWasSet(View.View view)
        {
            BaseOwnerView = view;
        }
        internal virtual void SetOwner(UnityEngine.Component owner)
        {
            _owner = owner;
        }

        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
        protected abstract void SolveDependencies();

        protected abstract void InternalAwake();
        protected abstract void InternalStart();
        protected abstract void InternalOnDestroy();
        protected abstract void InternalOnEnable();
        protected abstract void InternalOnDisable();

        private void Awake()
        {
            SolveDependencies();
            InternalAwake();
            MVCAwake();
        }
        private void Start()
        {
            InternalStart();
            StartCoroutine(LateStartRoutine());
            MVCStart();
        }
        private void Update()
        {
            MVCUpdate();
        }
        private void OnEnable()
        {
            StartCoroutine(LateEnable());
        }

        private IEnumerator LateEnable()
        {
            yield return null;
            InternalOnEnable();
            MVCOnEnable();
        }
        private void OnDisable()
        {
            InternalOnDisable();
            MVCOnDisable();
        }
        private void OnDestroy()
        {
            MVCOnDestroy();
            InternalOnDestroy();
        }

        protected virtual void MVCAwake() {}
        protected virtual void MVCStart() {}
        protected virtual void MVCUpdate() {}
        protected virtual void MVCLateStart() {}
        protected virtual void MVCOnEnable() {}
        protected virtual void MVCOnDisable() {}
        protected virtual void MVCOnDestroy() {}

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            MVCLateStart();
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
        
        public T GetUnityComponentInChildrenFromMVC<T>(bool includeHidden = false, bool addToStoredComponentsList = false) where T : UnityEngine.Component
        {
            T component = _unityComponents.FirstOrDefault(x => x.GetType() == typeof(T)) as T;

            if (component == null)
            {
                component = GetComponentInChildren<T>(includeHidden);
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
        
        public List<T> GetUnityComponentsInChildrenFromMVC<T>(bool includeHidden = false, bool addToStoredComponentsList = false) where T : UnityEngine.Component
        {
            List<T> components = _unityComponents.Where(x => x.GetType() == typeof(T)) as List<T>;

            if (components.Count == 0)
            {
                components = GetComponentsInChildren<T>(includeHidden).ToList();
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