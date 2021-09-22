using System;

namespace UnityMVC.Events
{
    public partial class ControllerTemplateEvents : MVCEvents
    {
        // Add events here
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

/*<NAMESPACE>*/
    public partial class ControllerTemplate
    {
        //// MVC properties available: View and Events

        // Start your code here
        protected override void SolveDependencies()
        {
            // Awake calls this method. Solve your dependencies here.
            //base.SolveDependencies();
        }
        
        protected override void RegisterEvents()
        {
            // otherObject.EventName += MyMethod;
            //base.RegisterEvents();
        }
        
        protected override void UnregisterEvents()
        {
            // otherObject.EventName -= MyMethod;
            //base.UnregisterEvents();
        }

        protected override void MVCAwake()
        {
            // Start your code from here
            //base.MVCAwake();
        }

        protected override void MVCStart()
        {
            // Start your code from here
            //base.MVCStart();
        }

        protected override void MVCLateStart()
        {
            // Start your code from here
            //base.MVCLateStart();
        }

        protected override void MVCUpdate()
        {
            // Start your code from here
            //base.MVCUpdate();
        }
    }
/*}*/