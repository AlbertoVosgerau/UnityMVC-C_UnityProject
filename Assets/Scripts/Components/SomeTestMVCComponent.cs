using System;
using UnityMVC;

public class SomeTestMVCComponentEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public partial class SomeTestMVCComponent
{
    private ViewTemplate _view => _baseView as ViewTemplate;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestMVCComponentEvents Events => _events;
    private SomeTestMVCComponentEvents _events = new SomeTestMVCComponentEvents();
    
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
    
    protected override void Awake()
    {
        base.Awake();
        // Add your code from here
    }
    
    protected override void Start()
    {
        base.Start();
        // Add your code from here
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // Add your code from here
    }
}