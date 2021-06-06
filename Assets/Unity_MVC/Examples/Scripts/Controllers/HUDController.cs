using System;
using UnityEngine;
using UnityMVC;

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
        _matchController = MVC.Controllers.Get<MatchController>();
        _matchController.Events.onPointsChanged += OnPointsUpdated;
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()    
    {
        base.OnViewDestroy();
        _matchController.Events.onPointsChanged -= OnPointsUpdated;
    }

    private void OnPointsUpdated(int points)
    {
        Debug.Log($"Calling event with points {points}");
        Events.onPointsUpdated?.Invoke(points);
    }
}