using System;
using System.Collections.Generic;
using UnityMVC;

public class ViewTemplateEvents
{
    // Add events here
    public Action<View> onViewDestroyed;
}

public partial class ViewTemplate : View
{
    private ControllerTemplate _controller;
    protected override void LocateController()
    {
        _controller = MVCApplication.Controllers.Get<ControllerTemplate>();
    }
    
    protected override void InitializeController()
    {
        _controller.SetView(this);
        _controller.OnInitializeController();
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ViewTemplateEvents Events => _events;
    private ViewTemplateEvents _events = new ViewTemplateEvents();

    protected override void MVCStart()
    {
        base.MVCStart();
        _controller.OnViewStart();
    }

    protected override void MVCOnDestroy()
    {
        base.MVCOnDestroy();
        _events.onViewDestroyed?.Invoke(this);
        _controller.OnViewDestroy();
    }

    protected override void MVCUpdate()
    {
        _controller.OnViewUpdate();
    }
}

public partial class ViewTemplate
{
    // Start your code here
    protected override void RegisterControllerEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected override void UnregisterControllerEvents()
    {
        // otherObject.EventName -= MyMethod;
    }

    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}