using System.Collections.Generic;
using UnityMVC;

public class SomeTestViewEvents
{
    // Add events here
}

public partial class SomeTestView : View
{
    private SomeTestController _controller;
    protected override void LocateController()
    {
        _controller = MVCApplication.Controllers.Get<SomeTestController>();
    }
    
    protected override void InitializeController()
    {
        _controller.SetView(this);
        _controller.OnInitializeController();
    }
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestViewEvents Events => _events;
    private SomeTestViewEvents _events = new SomeTestViewEvents();

    protected override void MVCStart()
    {
        base.MVCStart();
        _controller.OnViewStart();
    }

    protected override void MVCOnDestroy()
    {
        base.MVCOnDestroy();
        _controller.OnViewDestroy();
    }

    protected void MVCUpdate()
    {
        _controller.OnViewUpdate();
    }
}

public partial class SomeTestView
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
        MVCUpdate();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
}