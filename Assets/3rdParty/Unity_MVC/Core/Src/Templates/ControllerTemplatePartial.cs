using UnityMVC;

public partial class ControllerTemplate : Controller
{
    
    public override void SetView(View view)
    {
        _view = view as ViewTemplate;
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
