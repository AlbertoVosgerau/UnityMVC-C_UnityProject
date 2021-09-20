using System;
using AAAAA;

namespace UnityMVC.Events
{
    public partial class BBBBBControllerEvents
    {
        public Action<View.View> onViewEnabled;
        public Action<View.View> onViewDisabled;
        public Action<View.View> onViewDestroyed;
    }
}

namespace UnityMVC.Model
{
    public class BBBBBViewModel : MVCModel
    {
    }
}

namespace bbbbb
{
    public partial class BBBBBView
    {
        // MVC properties available: Controller, Events and Data
        private AAAAController _a;

        protected override void SolveDependencies()
        {
        }
        
        protected override void RegisterControllerEvents()
        {
        }

        protected override void UnregisterControllerEvents()
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