using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class ViewTemplateEvents
    {
        // Add events here
        public Action<View.View> onViewDestroyed;
    }
}

public partial class ViewTemplate
{
    private ControllerTemplate _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public /*new*/ ViewTemplateEvents Events => _events;
    private ViewTemplateEvents _events = new ViewTemplateEvents();
    
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

    protected override void StartMVC()
    {
        // Add your code here
    }

    protected override void LateStartMVC()
    {
        // Add your code here
    }

    protected override void UpdateMVC()
    {
        // Add your code here
    }
}