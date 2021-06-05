using System;
using UnityEngine;
using UnityMVC;
public class HUDView : View
{
    private HUDController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<HUDController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }
    
    // Start your code here
    private void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        StartController();
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