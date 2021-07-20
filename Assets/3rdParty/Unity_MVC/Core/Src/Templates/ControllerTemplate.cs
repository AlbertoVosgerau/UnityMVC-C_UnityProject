using System;
using UnityMVC;

public class ControllerTemplateEvents
{
    // Add events here
    public Action<Controller> onControllerInitialized;
    public Action<Controller> onControllerDestroyed;
}

public partial class ControllerTemplate : Controller
{
    private ViewTemplate _view;
    public override void SetView(View view)
    {
        _view = view as ViewTemplate;
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ControllerTemplateEvents Events => _events;
    private ControllerTemplateEvents _events = new ControllerTemplateEvents();
}

public partial class ControllerTemplate
{
    // Start your code here
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    public override void OnInitializeController()
    {
        base.OnInitializeController();
        RegisterEvents();
        _events.onControllerInitialized?.Invoke(this);
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
        _events.onControllerDestroyed?.Invoke(this);
        UnregisterEvents();
        base.OnViewDestroy();
    }
}