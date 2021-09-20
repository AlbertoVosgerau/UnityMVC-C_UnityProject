using System;
using SecondModuleNamespace;
using UnityMVC;
using UnityMVC.Controller;

namespace UnityMVC.Events
{
    public partial class NewModuleControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace NewNamespace
{
    public partial class NewModuleController
    {
        // MVC properties available: View and Events
        private SecondModuleController _second;

        protected override void SolveDependencies()
        {
            _second = MVCApplication.Controllers.Get<SecondModuleController>();
        }
        
        protected override void RegisterEvents()
        {
        }
        
        protected override void UnregisterEvents()
        {
        }

        protected override void MVCAwake()
        {
        }

        protected override void MVCStart()
        {
        }

        protected override void MVCLateStart()
        {
        }

        protected override void MVCUpdate()
        {
        }
    }
}