using UnityEngine;
using UnityEngine.UI;
using UnityMVC;

/// <summary>
/// Provides the Monobehaviour functionalities and data to its Controller
/// </summary>
public class HUDView : View
{
    private HUDController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<HUDController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        _controller.Events.onScoreUpdated += UpdatePoints;
        _controller.Events.onBestScoreSet += OnBestScoreSet;
    }

    protected override void UnregisterControllerEvents()
    {
        _controller.Events.onScoreUpdated -= UpdatePoints;
        _controller.Events.onBestScoreSet -= OnBestScoreSet;
    }
    
    // Start your code here
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    private void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
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

    private void OnBestScoreSet(int points)
    {
        _bestScoreText.text = $"Best: {points.ToString()}";
    }

    private void UpdatePoints(int points)
    {
        _scoreText.text = points.ToString();
    }
}