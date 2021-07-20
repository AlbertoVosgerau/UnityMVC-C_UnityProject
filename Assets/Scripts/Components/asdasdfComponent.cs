using System;
using UnityMVC;

public class asdasdfComponentEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public partial class asdasdfComponent : Component
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public asdasdfComponentEvents Events => _events;
    private asdasdfComponentEvents _events = new asdasdfComponentEvents();
}

public partial class asdasdfComponent
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