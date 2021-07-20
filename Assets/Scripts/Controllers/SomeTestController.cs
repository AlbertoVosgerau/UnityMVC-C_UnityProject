using System;
using UnityMVC;

public class SomeTestControllerEvents
{
    // Add events here
    public Action<Controller> onControllerInitialized;
    public Action<Controller> onControllerDestroyed;
}
public partial class SomeTestController
{
    private SomeTestView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestControllerEvents Events => _events;
    private SomeTestControllerEvents _events = new SomeTestControllerEvents();
    
    // Start your code here
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    public override void OnInitializeController()
    {
        base.OnInitializeController();
        //Start your code from here
    }

    public override void OnViewStart()
    {
        base.OnViewStart();
        // Start your code from here
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
        // Start your code from here
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        // Start your code from here
    }
}