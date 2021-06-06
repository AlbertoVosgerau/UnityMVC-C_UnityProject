using System;
using UnityEngine;
using UnityMVC;
using Object = UnityEngine.Object;

/// <summary>
/// Controllers handle business logic.
/// Meaning that all the main logic of a game will be defined by controllers.
/// This Controller handle the match state, points and player creation
/// Note: Maybe the BallSpawner could fit inside of this controller, but it seems better for organization
/// and it fits better the single responsibility requirements.
/// </summary>

public class MatchControllerEvents
{
    public Action<int> onPointsChanged;
}

public class MatchController : Controller
{
    private MatchView _view;
    public override void SetView(View view)
    {
        _view = view as MatchView;
    }
    public MatchControllerEvents Events => _events;
    private MatchControllerEvents _events = new MatchControllerEvents();
    // Start your code here
    private BallSpawnerController _ballSpawnerController;
    public int Points => _points;
    private int _points = 0;

    private PlayerComponent _player;

    public override void OnViewStart()
    {
        base.OnViewStart();
        CreatePlayer();
        CreateSpawner();
        _player.Events.onGotTheBall += OnPlayerGotTheBall;
    }

    protected override void SolveDependencies()
    {
        _ballSpawnerController = MVC.Controllers.Get<BallSpawnerController>();
    }

    private void CreateSpawner()
    {
        _ballSpawnerController.CreateBallSpawner();
    }
    
    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        _player.Events.onGotTheBall -= OnPlayerGotTheBall;
    }

    public void CreatePlayer()
    {
        _player = Object.Instantiate(_view.GetPlayerPrefab());
    }

    public void OnPlayerGotTheBall()
    {
        _points++;
        Events.onPointsChanged?.Invoke(_points);
    }
}