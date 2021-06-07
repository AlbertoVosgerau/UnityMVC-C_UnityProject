using System;
using System.Collections;
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
    public Action<int> onBestScoreSet;
    public Action<int> onScoreChanged;
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
    private PlayerController _playerController;
    private GameDataContainer _gameDataContainer;
    public int Points => _points;
    private int _points = 0;

    private PlayerComponent _player;

    public override void OnViewStart()
    {
        base.OnViewStart();
        CreateSpawner();
        RegisterEvents();
        
    }

    protected override void LateStart()
    {
        SetBestScore(_gameDataContainer.BestScore);
    }

    protected virtual void RegisterEvents()
    {
        _playerController.Events.onPlayerHitTheBall += OnPlayerGotTheBall;
    }

    protected virtual void UnregisterEvents()
    {
        _playerController.Events.onPlayerHitTheBall -= OnPlayerGotTheBall;
    }

    protected override void SolveDependencies()
    {
        _ballSpawnerController = MVC.Controllers.Get<BallSpawnerController>();
        _playerController = MVC.Controllers.Get<PlayerController>();
        _gameDataContainer = MVC.Containers.Get<GameDataContainer>();
        _player = _playerController.Player;
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        UnregisterEvents();
        if (_gameDataContainer.BestScore < _points)
        {
            _gameDataContainer.SaveScore(_points);
        }
    }

    private void CreateSpawner()
    {
        _ballSpawnerController.CreateBallSpawner();
    }
    
    private void SetBestScore(int points)
    {
        _events.onBestScoreSet?.Invoke(points);
    }

    public void OnPlayerGotTheBall()
    {
        _points++;
        _events.onScoreChanged?.Invoke(_points);
        if (_gameDataContainer.BestScore < _points)
        {
            _gameDataContainer.SaveScore(_points);
            _events.onBestScoreSet?.Invoke(_points);
        }
    }
}