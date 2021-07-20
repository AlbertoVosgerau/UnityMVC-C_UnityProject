using UnityMVC;

public class SomeTestSolverEvents
{
    // Add events here
}

public class SomeTestSolver : Solver
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SomeTestSolverEvents Events => _events;
    private SomeTestSolverEvents _events = new SomeTestSolverEvents();
    
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