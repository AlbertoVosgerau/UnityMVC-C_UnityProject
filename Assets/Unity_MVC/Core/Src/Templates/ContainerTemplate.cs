using UnityMVC;

public class ContainerTemplate : Container
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