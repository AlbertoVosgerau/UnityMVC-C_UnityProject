using System;
using UnityMVC;

public class ComponentTemplateEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public class ComponentTemplateInfo
{
    // Add metadata here
}

public class ComponentTemplate : Component
{
    public ComponentTemplateInfo Info => _info;
    private ComponentTemplateInfo _info = new ComponentTemplateInfo();
    public ComponentTemplateEvents Events => _events;
    private ComponentTemplateEvents _events = new ComponentTemplateEvents();

    // Start your code here
    protected override void Awake()
    {
        base.Awake();
        _events.onCreated?.Invoke();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnDestroy()
    {
        _events.onDestroyed?.Invoke();
    }

    protected override void SolveDependencies()
    {
        // Solve private dependencies
    }
}