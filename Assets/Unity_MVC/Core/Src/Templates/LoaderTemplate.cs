using UnityMVC;
public class LoaderTemplate : Loader
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
}