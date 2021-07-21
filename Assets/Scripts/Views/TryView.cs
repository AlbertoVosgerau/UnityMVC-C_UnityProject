using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class TryViewEvents
    {
        // Add events here
        public Action<View.View> onViewDestroyed;
    }
}

public partial class TryView
{
    private TryController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public TryViewEvents Events => _events;
    private TryViewEvents _events = new TryViewEvents();
    
    // Start your code here
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    protected override void RegisterControllerEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected override void UnregisterControllerEvents()
    {
        // otherObject.EventName -= MyMethod;
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