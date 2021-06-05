using System;
using UnityEngine;
using UnityMVC;
public class MatchView : View
{
    private MatchController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<MatchController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }
    
    // Start your code here
    [SerializeField] private PlayerComponent _playerPrefab;
    private void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        StartController();
    }

    protected void Update()
    {
        _controller.OnViewUpdate();
    }

    protected virtual void OnDestroy()
    {
        _controller.OnViewDestroy();
    }

    protected override void SolveDependencies()
    {
        base.SolveDependencies();
    }

    public PlayerComponent GetPlayerPrefab()
    {
        return _playerPrefab;
    }
}