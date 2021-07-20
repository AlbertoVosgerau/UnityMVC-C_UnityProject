using UnityMVC;

public partial class SomeTestController : Controller
{
    
    public override void SetView(View view)
    {
        _view = view as SomeTestView;
    }

    protected override void MVCOnInitializeController()
    {
        base.MVCOnInitializeController();
        RegisterEvents();
        _events.onControllerInitialized?.Invoke(this);
    }

    protected override void MVCOnViewDestroy()
    {
        base.MVCOnViewDestroy();
        _events.onControllerDestroyed?.Invoke(this);
        UnregisterEvents();
        base.OnViewDestroy();
    }
}
