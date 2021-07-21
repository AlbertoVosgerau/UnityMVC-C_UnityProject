using System;
using UnityMVC;

public class ControllerTemplateEvents
{
    // Add events here
    public Action<Controller> onControllerInitialized;
    public Action<Controller> onControllerDestroyed;
}

public partial class ControllerTemplate
{
    private ViewTemplate _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ControllerTemplateEvents Events => _events;
    private ControllerTemplateEvents _events = new ControllerTemplateEvents();
    
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