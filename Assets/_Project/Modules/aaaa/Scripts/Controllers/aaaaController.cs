using System;
using bbbb;

namespace UnityMVC.Events
{
    public partial class aaaaControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

namespace aaaa
{
    public partial class aaaaController
    {
        // MVC properties available: View and Events
        private bbbbView asdasd;
        private haha hasdas;
        private MVCaaaaaScriptableObjectScriptableObject a;

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