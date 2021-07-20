using System.Collections.Generic;
using UnityMVC;

public class ViewTemplateEvents
{
    // Add events here
}

public partial class ViewTemplate : View
{
    private ControllerTemplate _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<ControllerTemplate>();
    }
    
    protected override void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ViewTemplateEvents Events => _events;
    private ViewTemplateEvents _events = new ViewTemplateEvents();
}

public partial class ViewTemplate
{
    protected override void RegisterControllerEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected override void UnregisterControllerEvents()
    {
        // otherObject.EventName -= MyMethod;
    }

    // Start your code here
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        _controller.OnViewUpdate();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _controller.OnViewDestroy();
    }

    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
}