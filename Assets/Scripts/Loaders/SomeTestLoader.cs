using UnityMVC;

public class SomeTestLoaderEvents
{
    // Add events here
}

public partial class SomeTestLoader
{
    private SomeTestSolver _solver;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestLoaderEvents Events => _events;
    private SomeTestLoaderEvents _events = new SomeTestLoaderEvents();
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