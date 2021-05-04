using UnityMVC;
public class CounterView : View
{
    private CounterController controller => MVC.Controllers.Get<CounterController>();

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