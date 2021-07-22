using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class MainViewEvents
    {
        // Add events here
        public Action<View.View> onViewDestroyed;
    }
}

public partial class MainView
{
    private MainController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public MainViewEvents Events => _events;
    private MainViewEvents _events = new MainViewEvents();
    
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