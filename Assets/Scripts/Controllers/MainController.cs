using System;
using UnityEngine;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class MainControllerEvents
    {
        // Add events here
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class MainController
{
    private MainView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public MainControllerEvents Events => _events;
    private MainControllerEvents _events = new MainControllerEvents();
    
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
        Debug.Log("Hi! I'm the controller and I am alive!");
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