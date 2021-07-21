using UnityMVC;
using UnityMVC.Component;

public partial class ColorMVCComponent : MVCComponent
{
    protected override void MVCAwake()
    {
        base.MVCAwake();
        _events.onCreated?.Invoke();
    }

    protected override void MVCStart()
    {
        base.MVCStart();
        RegisterEvents();
    }
    
    protected override void MVCOnDestroy()
    {
        base.MVCOnDestroy();
        UnregisterEvents();
        _events.onDestroyed?.Invoke();
    }
}