using System;
using UnityEngine;
using UnityMVC;
public class HUDController : Controller
{
    private HUDView _view;
    public override void SetView(View view)
    {
        _view = view as HUDView;
    }
    
    // Start your code here
    private MatchController _matchController;
    
    private Action<int> onPointsUpdated;

    public override void OnViewStart()
    {
        base.OnViewStart();
        _matchController = MVC.Controllers.Get<MatchController>();
        Debug.Log($"{_matchController == null}");
        _matchController.onPointsChanged += OnPointsUpdated;
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()    
    {
        base.OnViewDestroy();
        _matchController.onPointsChanged -= OnPointsUpdated;
    }

    private void OnPointsUpdated(int points)
    {
        Debug.Log($"Got the ball. {points} points");
        onPointsUpdated?.Invoke(points);
    }
}