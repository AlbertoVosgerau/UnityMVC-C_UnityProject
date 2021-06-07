using System;
using UnityEngine;
using UnityMVC;

/// <summary>
/// Controllers handle business logic.
/// Meaning that all the main logic of a game will be defined by controllers.
/// This Controller handle the HUD that informs the player its points
/// </summary>
public class HUDControllerEvents
{
    public Action<int> onBestScoreSet;
    public Action<int> onScoreUpdated;
}

public class HUDController : Controller
{
    private HUDView _view;
    public override void SetView(View view)
    {
        _view = view as HUDView;
    }
    public HUDControllerEvents Events => _events;
    private HUDControllerEvents _events = new HUDControllerEvents();
    // Start your code here
    private MatchController _matchController;
    public override void OnViewStart()
    {
        base.OnViewStart();
        RegisterEvents();
    }

    protected override void SolveDependencies()
    {
        _matchController = MVC.Controllers.Get<MatchController>();
    }

    protected virtual void RegisterEvents()
    {
        _matchController.Events.onBestScoreSet += OnBestScoreSet;
        _matchController.Events.onScoreChanged += OnPointsUpdated;
    }

    protected virtual void UnregisterEvents()
    {
        _matchController.Events.onBestScoreSet -= OnBestScoreSet;
        _matchController.Events.onScoreChanged -= OnPointsUpdated;
    }

    public override void OnViewDestroy()    
    {
        base.OnViewDestroy();
        UnregisterEvents();
    }

    private void OnBestScoreSet(int points)
    {
        _events.onBestScoreSet?.Invoke(points);
    }

    private void OnPointsUpdated(int points)
    {
        _events.onScoreUpdated?.Invoke(points);
    }
}