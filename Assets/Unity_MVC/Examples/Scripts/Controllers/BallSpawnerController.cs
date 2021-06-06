using UnityEngine;
using UnityMVC;

public class BallSpawnerControllerEvents
{
    
}

public class BallSpawnerController : Controller
{
    private BallSpawnerView _view;
    public override void SetView(View view)
    {
        _view = view as BallSpawnerView;
    }
    public BallSpawnerControllerEvents Events => _events;
    private BallSpawnerControllerEvents _events = new BallSpawnerControllerEvents();
    // Start your code here
    private BallSpawnerComponent _ballSpawner;

    public override void OnViewStart()
    {
        base.OnViewStart();   
        CreateBallSpawner();
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }

    private void CreateBallSpawner()
    {
        _ballSpawner = Object.Instantiate(_view.GetBallSpawnerPrefab());
        _ballSpawner.StartSpawnRoutine();
    }
}