using System;
using UnityEngine;
using UnityMVC.Controller;
using UnityMVC.Events;
using UnityMVC.View;

namespace aaaa
{
    // Autogenerated code. DO NOT CHANGE unless it is really needed and you know what you are doing.
    public partial class aaaaController : Controller
    {
        private aaaaView View => _view;
        private aaaaView _view;
        public  aaaaControllerEvents Events => _events;
        private readonly aaaaControllerEvents _events = new aaaaControllerEvents();
        
        public override void SetView(View view)
        {
            if (_view != null)
            {
                Debug.LogException(new Exception($"More than one View are trying to access {this.GetType()}"));
                return;
            }
            _view = view as aaaaView;
        }

        public override View GetView()
        {
            return _view;
        }

        public override bool IsActive()
        {
            bool viewExists = _view != null;
            bool isActive = _view.gameObject.activeSelf;
            if (viewExists)
            {
                Debug.LogWarning($"View {typeof(aaaaView).Name} does not exist");
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
}