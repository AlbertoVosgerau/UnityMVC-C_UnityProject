using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC
{
    [Serializable]
    public abstract class MVCComponent : MonoBehaviour
    {
        public string View => _view.gameObject.name;
        protected View _view;
        public Component Owner => _owner;
        protected Component _owner;
        public void SetView(View view)
        {
            _view = view;
            OnViewWasSet(_view);
        }

        protected virtual void OnViewWasSet(View view)
        {
            
        }
        public virtual void SetOwner(Component owner)
        {
            _owner = owner;
        }
        
        [SerializeField] private List<Component> _unityComponents;

        protected virtual void Awake()
        {
            SolveDependencies();
        }

        protected virtual void MVCStart()
        {
            StartCoroutine(LateStartRoutine());
        }

        protected virtual void Start()
        {
            MVCStart();
        }

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStart();
        }

        protected virtual void LateStart()
        {
            
        }
        
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
        protected abstract void SolveDependencies();

        protected virtual void MVCOnDestroy()
        {
            _view.UnregisterComponentFromView(this);
        }
        protected virtual void OnDestroy()
        {
            MVCOnDestroy();
        }

        public void RegisterComponent(Component component)
        {
            _unityComponents.Add(component);
        }

        public void UnregisterComponent(Component component)
        {
            _unityComponents.Remove(component);
        }

        public T GetUnityComponentFromMVC<T>(bool addToStoredComponentsList = false) where T : Component
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
        
        public List<T> GetUnityComponentsFromMVC<T>(bool addToStoredComponentsList = false) where T : Component
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