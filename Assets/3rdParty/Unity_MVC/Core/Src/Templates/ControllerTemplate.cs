using UnityMVC;

public class ControllerTemplateEvents
{
    // Add events here
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
    
    protected virtual void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected virtual void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    
    public override void OnViewStart()
    {
        base.OnViewStart();
        RegisterEvents();
    }

    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        UnregisterEvents();
        base.OnViewDestroy();
    }
}