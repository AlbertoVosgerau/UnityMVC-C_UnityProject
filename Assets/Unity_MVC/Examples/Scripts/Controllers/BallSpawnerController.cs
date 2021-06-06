using UnityEngine;
using UnityMVC;

/// <summary>
/// Controllers handle business logic.
/// Meaning that all the main logic of a game will be defined by controllers.
/// This Controller handle the existence of the ball spawner and its functional state
/// </summary>

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
    
    public void CreateBallSpawner()
    {
        _ballSpawner = Object.Instantiate(_view.GetBallSpawnerPrefab());
        _ballSpawner.StartSpawnBallsRoutine();
    }
}