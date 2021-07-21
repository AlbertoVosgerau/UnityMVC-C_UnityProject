using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class TryControllerEvents
    {
        // Add events here
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class TryController
{
    private TryView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public TryControllerEvents Events => _events;
    private TryControllerEvents _events = new TryControllerEvents();
    
    // Start your code here
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }

    public override void OnInitializeController()
    {
        base.OnInitializeController();
        //Start your code from here
    }

    public override void OnViewStart()
    {
        base.OnViewStart();
        // Start your code from here
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
        // Start your code from here
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        // Start your code from here
    }
}