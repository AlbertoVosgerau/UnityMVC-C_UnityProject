using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityMVC.Component;

namespace UnityMVC.View
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] private List<MVCComponent> _MVCComponents = new List<MVCComponent>();
        protected abstract void LocateController();

        private void Awake()
        {
            SolveComponents();
            LocateController();
            SolveDependencies();
            InternalAwake();
            AwakeMVC();
        }

        private void Start()
        {
            InternalStart();
            ControllerStart();
            StartMVC();
        }
        
        private void Update()
        {
            ControllerUpdate();
            UpdateMVC();
        }

        private void OnEnable()
        {
            ControllerOnEnable();
            OnEnableMVC();
        }

        private void OnDisable()
        {
            ControllerOnDisable();
            OnDisableMVC();
        }

        private void OnDestroy()
        {
            ControllerOnDestroy();
            OnDestroyMVC();
        }

        protected virtual void InternalStart()
        {
            RegisterControllerEvents();
            StartCoroutine(LateStartRoutine());
        }

        protected virtual void InternalOnDestroy()
        {
            UnregisterControllerEvents();
        }
        
        protected virtual void AwakeMVC() {}
        protected virtual void StartMVC() {}
        protected virtual void UpdateMVC() {}
        protected virtual void LateStartMVC() {}
        protected virtual void OnEnableMVC() {}
        protected virtual void OnDisableMVC() {}
        protected virtual void OnDestroyMVC() {}

        protected abstract void ControllerStart();
        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStartMVC();
        }

        protected abstract void ControllerUpdate();

        protected abstract void ControllerOnEnable();

        protected abstract void ControllerOnDisable();

        protected abstract void ControllerOnDestroy();

        protected abstract void SolveDependencies();
        protected abstract void InternalAwake();
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
            foreach (MVCComponent component in _MVCComponents)
            {
                _MVCComponents.Add(component);
            }
        }

        private void GetComponents()
        {
            _MVCComponents = GetComponentsInChildren<MVCComponent>().ToList();
        }
        
        private void SetViewOnComponents()
        {
            foreach (MVCComponent component in _MVCComponents)
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
            return _MVCComponents.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }
        
        public List<T> GetMVCComponents<T>() where T : MVCComponent
        {
            return _MVCComponents.Where(x => x.GetType() == typeof(T)) as List<T>;
        }

        public void RegisterComponentToView(MVCComponent mvcComponent)
        {
            _MVCComponents.Add(mvcComponent);
        }
        
        public void UnregisterComponentFromView(MVCComponent mvcComponent)
        {
            _MVCComponents.Remove(mvcComponent);
        }
    }
}