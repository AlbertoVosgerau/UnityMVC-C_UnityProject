using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class LoaderTemplateEvents
    {
        // Add events here
    }
}

public partial class LoaderTemplate
{
    private SolverTemplate _solver;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public LoaderTemplateEvents Events => _events;
    private LoaderTemplateEvents _events = new LoaderTemplateEvents();
    // Start your code here
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
}