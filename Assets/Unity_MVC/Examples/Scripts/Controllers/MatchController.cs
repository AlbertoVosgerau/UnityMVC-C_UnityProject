using System;
using UnityEngine;
using UnityMVC;
using Object = UnityEngine.Object;

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
    public int Points => _points;
    private int _points = 0;

    private PlayerComponent _player;

    public override void OnViewStart()
    {
        base.OnViewStart();
        CreatePlayer();
        _player.Events.onGotTheBall += OnPlayerGotTheBall;
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
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