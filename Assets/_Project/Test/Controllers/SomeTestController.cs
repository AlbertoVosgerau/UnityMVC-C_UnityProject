using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public partial class SomeTestControllerEvents : MVCEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class SomeTestController
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