using UnityMVC;

public class AnotherControllerEvents
{
    // Add events here
}

public class AnotherController : Controller
{
    private AnotherView _view;
    public override void SetView(View view)
    {
        _view = view as AnotherView;
    }
    
    public AnotherControllerEvents Events => _events;
    private AnotherControllerEvents _events = new AnotherControllerEvents();
    
    // Start your code here

    public override void OnViewStart()
    {
        base.OnViewStart();   
    }

    protected override void SolveDependencies()
    {
        
    }

    protected virtual void RegisterEvents()
    {
        
    }

    protected virtual void UnregisterEvents()
    {
        
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