using UnityMVC;
public class CounterView : View
{
    private CounterController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<CounterController>();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }

    protected virtual void Start()
    {
        _controller.OnViewStart();
    }

    protected virtual void OnDestroy()
    {
        _controller.OnViewDestroy();
    }
}