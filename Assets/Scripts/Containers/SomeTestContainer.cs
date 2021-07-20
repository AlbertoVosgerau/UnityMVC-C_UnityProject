using UnityMVC;

public class SomeTestContainerEvents
{
    // Add events here
}

public partial class SomeTestContainer
{
    private SomeTestLoader _loader;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestContainerEvents Events => _events;
    private SomeTestContainerEvents _events = new SomeTestContainerEvents();
    
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