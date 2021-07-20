using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC
{
    public abstract class View : MonoBehaviour
    {
        private List<MVCComponent> _components = new List<MVCComponent>();
        [SerializeField] private List<MVCComponent> _MVCComponentsToTrack = new List<MVCComponent>();
        protected abstract void LocateController();

        protected virtual void Awake()
        {
            SolveComponents();
            LocateController();
            SolveDependencies();
            InitializeController();
        }

        protected virtual void MVCStart()
        {
            RegisterControllerEvents();
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

        protected void MakePersistent()
        {
            transform.parent = null;
            DontDestroyOnLoad(this);
        }

        protected virtual void MVCOnDestroy()
        {
            UnregisterControllerEvents();
        }

        protected virtual void OnDestroy()
        {
            MVCOnDestroy();
        }

        protected virtual void SolveDependencies(){}

        protected abstract void InitializeController();
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
            foreach (MVCComponent component in _MVCComponentsToTrack)
            {
                _components.Add(component);
            }
        }

        private void GetComponents()
        {
            _components = GetComponentsInChildren<MVCComponent>().ToList();
        }
        
        private void SetViewOnComponents()
        {
            foreach (MVCComponent component in _components)
            {
                component.SetView(this);
            }
        }

        public T AddMVCComponent<T>(GameObject gameObject) where T : MVCComponent, new()
        {
            T newComponent = gameObject.AddComponent<T>();
            RegisterComponentToView(newComponent);
            return newComponent as T;
        }
        
        public T GetMVCComponent<T>() where T : MVCComponent
        {
            return _components.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }
        
        public List<T> GetMVCComponents<T>() where T : MVCComponent
        {
            return _components.Where(x => x.GetType() == typeof(T)) as List<T>;
        }

        public void RegisterComponentToView(MVCComponent mvcComponent)
        {
            _components.Add(mvcComponent);
        }
        
        public void UnregisterComponentFromView(MVCComponent mvcComponent)
        {
            _components.Remove(mvcComponent);
        }
    }
}