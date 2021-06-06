using TMPro;
using UnityEngine;
using UnityMVC;
public class HUDView : View
{
    private HUDController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<HUDController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        _controller.Events.onPointsUpdated += UpdatePoints;
    }

    protected override void UnregisterControllerEvents()
    {
        _controller.Events.onPointsUpdated -= UpdatePoints;
    }
    
    // Start your code here
    [SerializeField] private TextMeshProUGUI _textMesh;
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
    
    private void UpdatePoints(int points)
    {
        Debug.Log($"Points: {points}");
        _textMesh.text = points.ToString();
    }
}