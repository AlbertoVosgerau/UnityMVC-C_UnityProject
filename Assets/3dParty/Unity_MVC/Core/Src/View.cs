using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.View
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] private List<MVCComponent> _MVCComponents = new List<MVCComponent>();
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

        protected virtual void MVCUpdate()
        {
            
        }

        protected virtual void Update()
        {
            MVCUpdate();
        }
        
        protected abstract void SolveDependencies();
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