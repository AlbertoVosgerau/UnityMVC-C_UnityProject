using System;
using bbbbb;

namespace UnityMVC.Events
{
    public partial class AAAAControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace AAAAA
{
    public partial class AAAAController
    {
        // MVC properties available: View and Events
        private BBBBBController _bbbb;

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