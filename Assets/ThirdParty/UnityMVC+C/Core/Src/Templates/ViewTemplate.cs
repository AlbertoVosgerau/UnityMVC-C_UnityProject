using System;

namespace UnityMVC.Events
{
    public partial class ControllerTemplateEvents
    {
        // Add events here
        //#=
        public Action<View.View> onViewEnabled;
        public Action<View.View> onViewDisabled;
        public Action<View.View> onViewDestroyed;
        //#
    }
}

namespace UnityMVC.Model
{
    public class ViewTemplateModel : MVCModel
    {
        // Add data here
    }
}

/*<NAMESPACE>*/
    public partial class ViewTemplate
    {
        //// MVC properties available: Controller, Events and Data

        // Start your code here
        protected override void SolveDependencies()
        {
            // Awake calls this method. Solve your dependencies here.
            //base.SolveDependencies();
        }
        
        protected override void RegisterControllerEvents()
        {
            // otherObject.EventName += MyMethod;
            //base.RegisterControllerEvents();
        }

        protected override void UnregisterControllerEvents()
        {
            // otherObject.EventName -= MyMethod;
            //base.UnregisterControllerEvents();
        }

        protected override void MVCAwake()
        {
            // Add your code here
            //base.MVCAwake();
        }

        protected override void MVCStart()
        {
            // Add your code here
            //base.MVCStart();
        }

        protected override void MVCLateStart()
        {
            // Add your code here
            //base.MVCLateStart();
        }

        protected override void MVCUpdate()
        {
            // Add your code here
            //base.MVCUpdate();
        }
    }
/*}*/