using UnityMVC;

public class TestContainer : Container
{
    public SolverTemplate Solver
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