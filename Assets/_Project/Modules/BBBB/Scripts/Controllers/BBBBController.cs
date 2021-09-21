using System;
using aaaaa;

namespace UnityMVC.Events
{
    public partial class BBBBControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace bbbbb
{
    public partial class BBBBController
    {
        // MVC properties available: View and Events
        private AAAAAController _aaaaaController;
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