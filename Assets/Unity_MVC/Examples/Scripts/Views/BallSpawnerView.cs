using System;
using UnityEngine;
using UnityMVC;
public class BallSpawnerView : View
{
    private BallSpawnerController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<BallSpawnerController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }
    
    // Start your code here
    [SerializeField] private BallSpawnerComponent _ballSpawnerPrefab;
    private void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        StartController();
    }

    protected void Update()
    {
        _controller.OnViewUpdate();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _controller.OnViewDestroy();
    }

    public BallSpawnerComponent GetBallSpawnerPrefab()
    {
        return _ballSpawnerPrefab;
    }
}