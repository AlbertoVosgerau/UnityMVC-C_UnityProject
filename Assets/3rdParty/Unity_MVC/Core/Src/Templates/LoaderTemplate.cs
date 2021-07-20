using UnityMVC;

public class LoaderTemplateEvents
{
    // Add events here
}

public partial class LoaderTemplate : Loader
{
    protected SolverTemplate Solver
    {
        get
        {
            if (_solver != null)
            {
                return _solver;
            }
            _solver = MVC.Solvers.Get<SolverTemplate>();
            return _solver;
        }
    }
    protected SolverTemplate _solver;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public LoaderTemplateEvents Events => _events;
    private LoaderTemplateEvents _events = new LoaderTemplateEvents();
}

public partial class LoaderTemplate
{
    // Start your code here
}