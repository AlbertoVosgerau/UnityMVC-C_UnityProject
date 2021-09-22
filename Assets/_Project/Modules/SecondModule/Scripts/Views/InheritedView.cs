using System;

namespace UnityMVC.Model
{
    public class InheritedViewModel : SecondModuleViewModel
    {
    }
}

namespace SecondNamespace
{
    public partial class InheritedView
    {
        // MVC properties available: Controller, Events and Data

        protected override void SolveDependencies()
        {
            base.SolveDependencies();
        }
        
        protected override void RegisterControllerEvents()
        {
            base.RegisterControllerEvents();
        }

        protected override void UnregisterControllerEvents()
        {
            base.UnregisterControllerEvents();
        }

        protected override void MVCAwake()
        {
            base.MVCAwake();
        }

        protected override void MVCStart()
        {
            base.MVCStart();
        }

        protected override void MVCLateStart()
        {
            base.MVCLateStart();
        }

        protected override void MVCUpdate()
        {
            base.MVCUpdate();
        }
    }
}