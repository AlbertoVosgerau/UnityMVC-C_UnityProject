using UnityMVC;

public class SomeTestControllerEvents
{
    // Add events here
}

public partial class SomeTestController : Controller
{
    private SomeTestView _view;
    public override void SetView(View view)
    {
        _view = view as SomeTestView;
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestControllerEvents Events => _events;
    private SomeTestControllerEvents _events = new SomeTestControllerEvents();
}

public partial class SomeTestController
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
    
    public override void OnInitializeController()
    {
        base.OnInitializeController();
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