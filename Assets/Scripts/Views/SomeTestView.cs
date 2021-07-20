using System;
using System.Collections.Generic;
using UnityMVC;

public class SomeTestViewEvents
{
    // Add events here
    public Action<View> onViewDestroyed;
}

public partial class SomeTestView
{
    private SomeTestController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestViewEvents Events => _events;
    private SomeTestViewEvents _events = new SomeTestViewEvents();
    
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