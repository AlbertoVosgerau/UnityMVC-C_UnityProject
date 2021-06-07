using System;
using UnityEngine;
using UnityMVC;

/// <summary>
/// Provides the Monobehaviour functionalities and data to its Controller
/// </summary>
public class MatchView : View
{
    private MatchController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<MatchController>();
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
}