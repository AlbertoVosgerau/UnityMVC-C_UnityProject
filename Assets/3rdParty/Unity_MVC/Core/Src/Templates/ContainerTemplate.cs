using UnityMVC;

public class ContainerTemplateEvents
{
    // Add events here
}

public partial class ContainerTemplate
{
    private LoaderTemplate _loader;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ContainerTemplateEvents Events => _events;
    private ContainerTemplateEvents _events = new ContainerTemplateEvents();
    
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