using System;
using UnityEngine;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class InheritedControllerEvents
    {
        // Add events here
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class InheritedController
{
    private InheritedView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public InheritedControllerEvents Events => _events;
    private InheritedControllerEvents _events = new InheritedControllerEvents();
    
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

    protected override void StartMVC()
    {
        Debug.Log("Hi! I'm the INHERITED controller and I am alive!");
    }

    protected override void LateStartMVC()
    {
        // Start your code from here
    }

    public override void OnViewUpdate()
    {
        // Start your code from here
    }
}