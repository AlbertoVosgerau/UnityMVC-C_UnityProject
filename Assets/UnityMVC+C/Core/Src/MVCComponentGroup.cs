using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC.Component
{
    public class MVCComponentGroup : MVCComponent
    {
        [SerializeField] protected bool _includeHiddenComponents = false;
        [Tooltip("Find MVC Components on the whole scene on Awake. If all your components are children of the view GameObject, set it to false")]
        [SerializeField] bool searchOutsideHierarchy = false;

        public List<MVCComponent> MVCComponents => _MVCComponents;
        [SerializeField] private List<MVCComponent> _MVCComponents = new List<MVCComponent>();
        public override Type GetViewType()
        {
            return null;
        }
        
        public override bool IsActive()
        {
            return false;
        }

        public override void SetView(View.View view)
        {
        }

        protected override void RegisterEvents()
        {
        }

        protected override void UnregisterEvents()
        {
        }

        protected override void SolveDependencies()
        {
        }

        protected override void InternalAwake()
        {
        }

        protected override void InternalStart()
        {
        }

        protected override void InternalOnDestroy()
        {
        }

        protected override void InternalOnEnable()
        {
            
        }

        protected override void InternalOnDisable()
        {
            
        }

        public T AddMVCComponent<T>(GameObject gameObject) where T : MVCComponent, new()
        {
            T newComponent = gameObject.AddComponent<T>();
            return newComponent;
        }
    
        public T GetMVCComponent<T>(bool includeHidden = false, bool storeComponent = false) where T : MVCComponent
        {
            T component = _MVCComponents.FirstOrDefault(x => x is T) as T;
            
            if (component == null)
            {
                component = GetComponentInChildren<T>(includeHidden);
            }

            if (component == null)
            {
                return null;
            }
            
            if (storeComponent && !_MVCComponents.Contains(component))
            {
                _MVCComponents.Add(component);
            }
            
            return component;
        }
    
        public List<T> GetMVCComponents<T>(bool includeHidden = false, bool storeComponents = false) where T : MVCComponent
        {
            List<MVCComponent> filteredList = searchOutsideHierarchy? new List<MVCComponent>(FindObjectsOfType<T>(includeHidden))
                : new List<MVCComponent>(GetComponentsInChildren<T>(includeHidden));
        
            List<T> newList = new List<T>();

            foreach (MVCComponent component in filteredList)
            {
                newList.Add(component as T);
                if (storeComponents && !_MVCComponents.Contains(component))
                {
                    _MVCComponents.Add(component);
                }
            }
        
            return newList;
        }
    }
}