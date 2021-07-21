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
        
        [SerializeField] private List<UnityEngine.Component> _unityComponents;

        protected virtual void MVCAwake()
        {
            SolveDependencies();
        }
        protected virtual void Awake()
        {
            MVCAwake();
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
            _baseView.UnregisterComponentFromView(this);
        }
        protected virtual void OnDestroy()
        {
            MVCOnDestroy();
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