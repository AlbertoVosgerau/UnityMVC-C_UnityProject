using System;
using UnityEngine;
using UnityMVC.Controller;
using UnityMVC.View;

public partial class ColorController : Controller
{
    public override void SetView(View view)
    {
        if (_view != null)
        {
            Debug.LogException(new Exception($"More than one View are trying to access {this.GetType()}"));
            return;
        }
        _view = view as ColorView;
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
    }
}
