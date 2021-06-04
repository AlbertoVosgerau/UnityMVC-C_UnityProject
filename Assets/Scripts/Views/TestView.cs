using UnityEngine;
using UnityMVC;
public class TestView : View
{
    private TestController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<TestController>();
    }

    private TestContainer _myContainer;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }

    protected virtual void Start()
    {
        _controller.OnViewStart();
        _myContainer = MVC.Containers.Get<TestContainer>();
        Debug.Log($"My container's solver is null: {_myContainer.Solver == null}");
        if (_myContainer != null)
        {
            Debug.Log($"My contcontainer's solveraier is of type: {_myContainer.Solver.GetType()}");
        }
    }

    protected virtual void OnDestroy()
    {
        _controller.OnViewDestroy();
    }
}