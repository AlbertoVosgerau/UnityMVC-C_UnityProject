using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC
{
    [Serializable]
    public class MVCComponent : MonoBehaviour
    {
        protected View _view;
        public void SetView(View view)
        {
            _view = view;
            OnViewWasSet(_view);
        }

        protected virtual void OnViewWasSet(View view)
        {
            
        }
        
        [SerializeField] private List<UnityEngine.Component> _unityComponents;

        protected virtual void Awake()
        {
            SolveDependencies();
        }

        protected virtual void Start()
        {
            StartCoroutine(LateStartRoutine());
        }

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStart();
        }

        protected virtual void LateStart()
        {
            
        }

        protected virtual void SolveDependencies()
        {
            
        }

        protected virtual void OnDestroy()
        {
            _view.UnregisterComponentFromView(this);
        }

        public void RegisterComponent(UnityEngine.Component component)
        {
            _unityComponents.Add(component);
        }

        public void UnregisterComponent(UnityEngine.Component component)
        {
            _unityComponents.Remove(component);
        }

        public T GetComponentMVC<T>(bool addToStoredComponentsList = false) where T : UnityEngine.Component
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
    }
}