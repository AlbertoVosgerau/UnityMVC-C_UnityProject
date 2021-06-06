using System;
using UnityEngine;
using UnityMVC;
using Object = UnityEngine.Object;

public class MatchController : Controller
{
    private MatchView _view;
    public override void SetView(View view)
    {
        _view = view as MatchView;
    }
    
    // Start your code here
    public int Points => _points;
    private int _points = 0;

    private PlayerComponent _player;

    public Action<int> onPointsChanged;

    public override void OnViewStart()
    {
        base.OnViewStart();
        CreatePlayer();
        // TODO: Add method dedicated to register and unregister events
        _player.Controller.onGotTheBall += OnPlayerGotTheBall;
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        _player.Controller.onGotTheBall -= OnPlayerGotTheBall;
    }

    public void CreatePlayer()
    {
        _player = Object.Instantiate(_view.GetPlayerPrefab());
    }

    public void OnPlayerGotTheBall()
    {
        _points++;
        onPointsChanged?.Invoke(_points);
    }
}