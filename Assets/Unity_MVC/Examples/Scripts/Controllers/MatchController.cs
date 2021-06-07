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
    private GameDataContainer _gameDataContainer;
    public int Points => _points;
    private int _points = 0;

    private PlayerComponent _player;

    public override void OnViewStart()
    {
        base.OnViewStart();
        CreatePlayer();
        CreateSpawner();
        RegisterEvents();
        SetBestScore(_gameDataContainer.BestScore);
    }

    protected virtual void RegisterEvents()
    {
        _player.Events.onGotTheBall += OnPlayerGotTheBall;
    }

    protected virtual void UnregisterEvents()
    {
        _player.Events.onGotTheBall -= OnPlayerGotTheBall;
    }

    protected override void SolveDependencies()
    {
        _ballSpawnerController = MVC.Controllers.Get<BallSpawnerController>();
        _gameDataContainer = MVC.Containers.Get<GameDataContainer>();
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

    public void CreatePlayer()
    {
        _player = Object.Instantiate(_view.GetPlayerPrefab());
    }

    private void SetBestScore(int points)
    {
        CoroutineHelper.StartCoroutine(this,SetBestScoreRoutine(points));
    }

    private IEnumerator SetBestScoreRoutine(int points)
    {
        yield return null;
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