using UnityMVC;
public class SolverTemplate : Solver
{
    protected LoaderTemplate Solver
    {
        get
        {
            if (_solver != null)
            {
                return _solver;
            }

            _solver = MVC.Loaders.Get<LoaderTemplate>();
            return _solver;
        }
    }
    protected LoaderTemplate _solver;
}