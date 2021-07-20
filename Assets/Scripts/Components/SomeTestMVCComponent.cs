using System;
using UnityMVC;

public class SomeTestMVCComponentEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public partial class SomeTestMVCComponent : MVCComponent
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestMVCComponentEvents Events => _events;
    private SomeTestMVCComponentEvents _events = new SomeTestMVCComponentEvents();
}

public partial class SomeTestMVCComponent
{
    protected virtual void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected virtual void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    
    // Start your code here
    
    protected override void Awake()
    {
        base.Awake();
        _events.onCreated?.Invoke();
    }
    
    protected override void Start()
    {
        base.Start();
        RegisterEvents();
    }
    
    private void OnDestroy()
    {
        UnregisterEvents();
        _events.onDestroyed?.Invoke();
    }
    
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
}