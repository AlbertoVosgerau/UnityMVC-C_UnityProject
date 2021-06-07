using System;
using UnityMVC;
using Object = UnityEngine.Object;

public class PlayerControllerEvents
{
    public Action onPlayerHitTheBall;
}

public class PlayerController : Controller
{
    private PlayerView _view;
    public override void SetView(View view)
    {
        _view = view as PlayerView;
    }
    
    public PlayerControllerEvents Events => _events;
    private PlayerControllerEvents _events = new PlayerControllerEvents();
    
    // Start your code here
    public PlayerComponent Player
    {
        get
        {
            if (_player != null)
            {
                return _player;
            }
            _player = GetNewPlayer();
            return _player;
        }
    }
    private PlayerComponent _player;

    public override void OnViewStart()
    {
        base.OnViewStart();
        RegisterEvents();
    }

    protected override void SolveDependencies()
    {
        
    }

    protected virtual void RegisterEvents()
    {
        Player.Events.onGotTheBall += OnPlayerGotTheBall;
    }

    protected virtual void UnregisterEvents()
    {
        _player.Events.onGotTheBall -= OnPlayerGotTheBall;
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        UnregisterEvents();
        base.OnViewDestroy();
    }
    
    private PlayerComponent GetNewPlayer()
    {
        return Object.Instantiate(_view.GetPlayerPrefab());
    }
    
    public void OnPlayerGotTheBall()
    {
        _events.onPlayerHitTheBall?.Invoke();
    }
}