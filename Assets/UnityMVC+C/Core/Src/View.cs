using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.View
{
    public abstract class View : MonoBehaviour
    {
        [Tooltip("Find MVC Components on the whole scene on Awake. If all your components are children of the view GameObject, set it to false")]
        [SerializeField] protected bool searchOutsideHierarchy = true;
        [Tooltip("Include hidden components on the search on Awake")]
        [SerializeField] private bool _includeHiddenComponents = false;
        [Tooltip("Stores corresponding MVC Components for access trough GetMVCComponents method")]
        [SerializeField] private List<MVCComponent> _MVCComponents = new List<MVCComponent>();

        protected abstract void LocateController();

        public abstract bool IsActive();
        
        protected abstract void SolveDependencies();
        protected abstract void RegisterControllerEvents();
        protected abstract void UnregisterControllerEvents();
        
        protected abstract void InternalAwake();
        protected abstract void InternalStart();
        protected abstract void InternalOnDestroy();
        protected abstract void InternalOnEnable();
        protected abstract void InternalOnDisable();

        private void Awake()
        {
            SolveComponents();
            LocateController();
            SolveDependencies();
            InternalAwake();
            MVCAwake();
        }
        private void Start()
        {
            InternalStart();
            ControllerStart();
            MVCStart();
        }
        private void Update()
        {
            ControllerUpdate();
            MVCUpdate();
        }
        private void OnEnable()
        {
            ControllerOnEnable();
            InternalOnEnable();
            MVCOnEnable();
        }
        private void OnDisable()
        {
            ControllerOnDisable();
            InternalOnDisable();
            MVCOnDisable();
        }
        private void OnDestroy()
        {
            ControllerOnDestroy();
            MVCOnDestroy();
        }

        protected virtual void MVCAwake() {}
        protected virtual void MVCStart() {}
        protected virtual void MVCUpdate() {}
        protected virtual void MVCLateStart() {}
        protected virtual void MVCOnEnable() {}
        protected virtual void MVCOnDisable() {}
        protected virtual void MVCOnDestroy() {}

        protected abstract void ControllerStart();
        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            MVCLateStart();
        }

        protected abstract void ControllerUpdate();
        protected abstract void ControllerOnEnable();
        protected abstract void ControllerOnDisable();
        protected abstract void ControllerOnDestroy();
        
        private void SolveComponents()
        {
            GetComponents<MVCComponent>(_includeHiddenComponents);
            SetViewOnComponents();
        }
        private void GetComponents <T>(bool includeHidden = true) where T : MVCComponent
        {
            List<MVCComponent> localList = searchOutsideHierarchy? new List<MVCComponent>(FindObjectsOfType<T>(includeHidden))
                        : new List<MVCComponent>(GetComponentsInChildren<T>(includeHidden));
            
            foreach (MVCComponent component in localList)
            {
                if (!_MVCComponents.Contains(component) && component.GetViewType() == GetType())
                {
                    _MVCComponents.Add(component);
                }
            }
        }
        private void SetViewOnComponents()
        {
            foreach (MVCComponent component in _MVCComponents)
            {
                component.SetView(this);
            }
        }
        public void RegisterComponentToView(MVCComponent mvcComponent)
        {
            _MVCComponents.Add(mvcComponent);
        }
        public void UnregisterComponentFromView(MVCComponent mvcComponent)
        {
            _MVCComponents.Remove(mvcComponent);
        }
        
        public T AddMVCComponent<T>(GameObject gameObject) where T : MVCComponent, new()
        {
            T newComponent = gameObject.AddComponent<T>();
            RegisterComponentToView(newComponent);
            return newComponent;
        }
        public T GetMVCComponent<T>() where T : MVCComponent
        {
            return _MVCComponents.FirstOrDefault(x => x is T) as T;
        }
        public List<T> GetMVCComponents<T>() where T : MVCComponent
        {
            List<MVCComponent> filteredList = _MVCComponents.Where(x => x is T).ToList();
            List<T> newList = new List<T>();
            foreach (MVCComponent component in filteredList)
            {
                newList.Add(component as T);
            }
            
            return newList;
        }
    }
}