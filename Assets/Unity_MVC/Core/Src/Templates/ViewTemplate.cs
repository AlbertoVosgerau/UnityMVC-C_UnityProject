using System;
using UnityMVC;
public class ViewTemplate : View
{
    private ControllerTemplate _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<ControllerTemplate>();
    }
    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        _controller.OnViewStart();
    }

    protected void Update()
    {
        _controller.OnViewUpdate();
    }

    protected virtual void OnDestroy()
    {
        _controller.OnViewDestroy();
    }

    protected override void SolveDependencies()
    {
        base.SolveDependencies();
    }
}