using System.Collections.Generic;
using UnityMVC;

public class sViewEvents
{
    // Add events here
}

public partial class sView : View
{
    private sController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<sController>();
    }
    
    protected override void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public sViewEvents Events => _events;
    private sViewEvents _events = new sViewEvents();
}

public partial class sView
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