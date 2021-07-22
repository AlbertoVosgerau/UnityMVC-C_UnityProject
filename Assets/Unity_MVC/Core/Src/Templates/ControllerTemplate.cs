using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class ControllerTemplateEvents
    {
        // Add events here
        public Action<UnityMVC.Controller.Controller> onControllerInitialized;
        public Action<UnityMVC.Controller.Controller> onControllerDestroyed;
    }
}

public partial class ControllerTemplate
{
    private ViewTemplate _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public /*new*/ ControllerTemplateEvents Events => _events;
    private ControllerTemplateEvents _events = new ControllerTemplateEvents();
    
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
        // Start your code from here
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