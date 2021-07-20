﻿using System;
using UnityMVC;

public class MVCComponentTemplateEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public partial class MVCComponentTemplate : MVCComponent
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public MVCComponentTemplateEvents Events => _events;
    private MVCComponentTemplateEvents _events = new MVCComponentTemplateEvents();

    protected override void MVCStart()
    {
        base.MVCStart();
        RegisterEvents();
    }
    
    protected override void MVCOnDestroy()
    {
        base.MVCOnDestroy();
        UnregisterEvents();
        _events.onDestroyed?.Invoke();
    }
}

public partial class MVCComponentTemplate
{
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
        _events.onCreated?.Invoke();
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