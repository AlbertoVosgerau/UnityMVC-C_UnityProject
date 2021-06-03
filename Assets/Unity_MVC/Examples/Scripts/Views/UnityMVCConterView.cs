using System;
using UnityEngine;
using UnityMVC;
public class UnityMVCConterView : View
{

    private UnityMVCConterController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<UnityMVCConterController>();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _controller.LoadScene();
        }
    }

    protected virtual void OnDestroy()
    {
        _controller.OnViewDestroy();
    }
}