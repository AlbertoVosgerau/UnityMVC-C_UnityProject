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
    public Action<int> onPointsUpdated;
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
        _matchController.Events.onPointsChanged += OnPointsUpdated;
    }

    protected override void SolveDependencies()
    {
        _matchController = MVC.Controllers.Get<MatchController>();
    }

    public override void OnViewDestroy()    
    {
        base.OnViewDestroy();
        _matchController.Events.onPointsChanged -= OnPointsUpdated;
    }

    private void OnPointsUpdated(int points)
    {
        Events.onPointsUpdated?.Invoke(points);
    }
}