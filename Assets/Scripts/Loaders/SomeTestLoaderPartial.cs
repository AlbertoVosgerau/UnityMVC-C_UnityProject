using UnityMVC;

public partial class SomeTestLoader : Loader
{
    private SomeTestSolver Solver => _solver;
    public void Initialize()
    {
        if (_solver != null)
        {
            return;
        }
        _solver = MVCApplication.Solvers.Get<SomeTestSolver>();
    }
}
