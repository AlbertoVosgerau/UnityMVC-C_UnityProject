using UnityMVC;

public class sControllerEvents
{
    // Add events here
}

public partial class sController : Controller
{
    private sView _view;
    public override void SetView(View view)
    {
        _view = view as sView;
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public sControllerEvents Events => _events;
    private sControllerEvents _events = new sControllerEvents();
}

public partial class sController
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