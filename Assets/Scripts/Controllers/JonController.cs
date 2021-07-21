using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class JonControllerEvents
    {
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class JonController
{
    private JonView _view;
    
    public JonControllerEvents Events => _events;
    private JonControllerEvents _events = new JonControllerEvents();
    
    protected override void SolveDependencies()
    {
    }
    
    protected override void RegisterEvents()
    {
    }
    
    protected override void UnregisterEvents()
    {
    }

    public override void OnInitializeController()
    {
        base.OnInitializeController();
    }

    public override void OnViewStart()
    {
        base.OnViewStart();
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }
}