using System;
using UnityEngine;
using UnityMVC.Controller;
using UnityMVC.Events;
using UnityMVC.View;
using Object = System.Object;

/*<NAMESPACE>*/
    //// Autogenerated code. DO NOT CHANGE unless it is really needed and you know what you are doing.
    public partial class ControllerTemplate : Controller
    {
        private ViewTemplate View => _view;
        private ViewTemplate _view;
        // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
        public /*NEW*/ ControllerTemplateEvents Events => _events;
        private readonly ControllerTemplateEvents _events = new ControllerTemplateEvents();
        
        public override void SetView(View view)
        {
            if (_view != null)
            {
                if (_isDontDestroyOnLoad)
                {
                    UnityEngine.Object.Destroy(view.gameObject);
                    return;
                }
                
                Debug.LogException(new Exception($"More than one View are trying to access {this.GetType()}"));
                return;
            }
            _view = view as ViewTemplate;
        }

        public override View GetView()
        {
            return _view;
        }

        public override bool IsActive()
        {
            bool viewExists = _view != null;
            bool isActive = viewExists && _view.gameObject.activeSelf;
            if (viewExists)
            {
                Debug.LogWarning($"View {typeof(ViewTemplate).Name} does not exist");
            }
            if (!isActive)
            {
                Debug.LogWarning($"View {this.GetType().Name} not active");
            }
            return isActive && viewExists;
        }

        protected override void InternalAwake()
        {
            _events.onControllerInitialized?.Invoke(this);
        }
        protected override void InternalOnDestroy()
        {
            _events.onControllerDestroyed?.Invoke(this);
        }

        protected override void InternalOnEnable()
        {
            
        }

        protected override void InternalOnDisable()
        {
            
        }
    }
/*}*/