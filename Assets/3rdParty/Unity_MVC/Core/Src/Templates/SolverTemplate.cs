using UnityMVC;

public class SolverTemplateEvents
{
    // Add events here
}

public partial class SolverTemplate : Solver
{
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public SolverTemplateEvents Events => _events;
    private SolverTemplateEvents _events = new SolverTemplateEvents();
}
public partial class SolverTemplate
{
    // Start your code here
    protected override void UnregisterEvents()
    {
    }
    
    protected override void RegisterEvents()
    {
    }
}