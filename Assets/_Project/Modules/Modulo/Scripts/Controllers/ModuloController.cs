using System;

namespace UnityMVC.Events
{
    public partial class ModuloControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace ModuloNamespace
{
    public partial class ModuloController
    {
        // MVC properties available: View and Events

        protected override void SolveDependencies()
        {
        }
        
        protected override void RegisterEvents()
        {
        }
        
        protected override void UnregisterEvents()
        {
        }

        protected override void MVCAwake()
        {
            DontDestroyOnLoad(_view);
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