using UnityMVC;

public partial class LoaderTemplate : Loader
{
    private SolverTemplate Solver => _solver;
    public void Initialize()
    {
        if (_solver != null)
        {
            return;
        }
        _solver = MVCApplication.Solvers.Get<SolverTemplate>();
    }
}
