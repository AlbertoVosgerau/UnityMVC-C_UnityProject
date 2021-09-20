namespace UnityMVC.Events
{
    public partial class ModuleNameControllerEvents
    {
    }
}

namespace ModuleNamespace
{
    public partial class InheritedMVCComponent
    {
        // MVC properties available: View and Events

        protected override void SolveDependencies()
        {
            base.SolveDependencies();
        }
        
        protected override void RegisterEvents()
        {
            base.RegisterEvents();
        }
        
        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();
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