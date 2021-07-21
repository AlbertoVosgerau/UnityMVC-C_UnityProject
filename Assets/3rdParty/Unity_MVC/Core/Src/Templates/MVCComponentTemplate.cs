using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class MVCComponentTemplateEvents
    {
        // Add your actions and events here
        public Action onCreated;
        public Action onDestroyed;
    }
}

public partial class MVCComponentTemplate
{
    private ViewTemplate _view => _baseView as ViewTemplate;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public MVCComponentTemplateEvents Events => _events;
    private MVCComponentTemplateEvents _events = new MVCComponentTemplateEvents();
    
    // Start your code here
    
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
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