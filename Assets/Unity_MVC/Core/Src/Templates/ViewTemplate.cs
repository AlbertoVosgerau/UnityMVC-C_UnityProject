using UnityMVC;
public class ViewTemplate : View
{
    private ControllerTemplate controller => MVC.Controllers.Get<ControllerTemplate>();

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void RegisterControllerEvents()
    {
        base.RegisterControllerEvents();
    }

    protected override void UnregisterControllerEvents()
    {
        base.UnregisterControllerEvents();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}