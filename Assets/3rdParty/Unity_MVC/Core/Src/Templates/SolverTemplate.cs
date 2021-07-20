using UnityMVC;

public class SolverTemplateEvents
{
    // Add events here
}

public class SolverTemplate : Solver
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SolverTemplateEvents Events => _events;
    private SolverTemplateEvents _events = new SolverTemplateEvents();
    
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