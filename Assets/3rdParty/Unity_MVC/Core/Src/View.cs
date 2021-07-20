using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC
{
    public abstract class View : MonoBehaviour
    {
        private List<Component> _components = new List<Component>();
        [SerializeField] private List<Component> _MVCComponentsToTrack = new List<Component>();
        protected abstract void LocateController();

        protected virtual void Awake()
        {
            SolveComponents();
            LocateController();
            SolveDependencies();
            StartController();
        }

        protected virtual void Start()
        {
            RegisterControllerEvents();
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

        

        protected void MakePersistent()
        {
            transform.parent = null;
            DontDestroyOnLoad(this);
        }

        protected virtual void OnDestroy()
        {
            UnregisterControllerEvents();
        }

        protected virtual void SolveDependencies(){}

        protected abstract void StartController();
        protected abstract void RegisterControllerEvents();

        protected abstract void UnregisterControllerEvents();

        private void SolveComponents()
        {
            InitializeComponentsList();
            GetComponents();
            SetViewOnComponents();
        }

        private void InitializeComponentsList()
        {
            foreach (Component component in _MVCComponentsToTrack)
            {
                _components.Add(component);
            }
        }

        private void GetComponents()
        {
            _components = GetComponentsInChildren<Component>().ToList();
        }
        
        private void SetViewOnComponents()
        {
            foreach (Component component in _components)
            {
                component.SetView(this);
            }
        }

        public T AddComponentMVC<T>(GameObject gameObject) where T : Component, new()
        {
            T newComponent = gameObject.AddComponent<T>();
            RegisterComponentToView(newComponent);
            return newComponent as T;
        }
        
        public T GetComponentMVC<T>() where T : Component
        {
            return _components.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }

        public void RegisterComponentToView(Component component)
        {
            _components.Add(component);
        }
        
        public void UnregisterComponentFromView(Component component)
        {
            _components.Remove(component);
        }
    }
}