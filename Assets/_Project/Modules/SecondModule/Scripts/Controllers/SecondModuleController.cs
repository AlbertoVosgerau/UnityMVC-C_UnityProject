using System;
using NewNamespace;
using UnityMVC;

namespace UnityMVC.Events
{
    public partial class SecondModuleControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace SecondModuleNamespace
{
    public partial class SecondModuleController
    {
        // MVC properties available: View and Events
        private NewModuleController _newModuleController;

        protected override void SolveDependencies()
        {
            _newModuleController = MVCApplication.Controllers.Get<NewModuleController>();
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