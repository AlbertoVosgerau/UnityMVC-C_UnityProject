using System;
using bbbbb;

namespace UnityMVC.Events
{
    public partial class AAAAAControllerEvents
    {
        public Action<View.View> onViewEnabled;
        public Action<View.View> onViewDisabled;
        public Action<View.View> onViewDestroyed;
    }
}

namespace UnityMVC.Model
{
    public class AAAAAViewModel : MVCModel
    {
    }
}

namespace aaaaa
{
    public partial class AAAAAView
    {
        // MVC properties available: Controller, Events and Data
        private BBBBController _bbbb;

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