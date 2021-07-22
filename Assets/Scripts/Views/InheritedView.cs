using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class InheritedViewEvents
    {
        // Add events here
        public Action<View.View> onViewDestroyed;
    }
}

public partial class InheritedView
{
    private InheritedController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public new InheritedViewEvents Events => _events;
    private InheritedViewEvents _events = new InheritedViewEvents();
    
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