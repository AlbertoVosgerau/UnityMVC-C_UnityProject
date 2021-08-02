using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public partial class SomeTestControllerEvents
    {
        public Action<View.View> onViewDestroyed;
    }
}

namespace UnityMVC.Model
{
    public class SomeTestViewModel : MVCModel
    {
    }
}

public partial class SomeTestView
{
    // MVC properties available: Controller, Events and Data

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