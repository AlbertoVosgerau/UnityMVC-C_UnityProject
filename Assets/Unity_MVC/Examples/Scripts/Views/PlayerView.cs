using UnityEngine;
using UnityMVC;
public class PlayerView : View
{
    private PlayerController _controller;
    protected override void LocateController()
    {
        _controller = MVC.Controllers.Get<PlayerController>();
    }
    
    protected override void RegisterControllerEvents()
    {
        
    }

    protected override void UnregisterControllerEvents()
    {

    }
    private void StartController()
    {
        _controller.SetView(this);
        _controller.OnViewStart();
    }
    
    // Start your code here
    [SerializeField] private PlayerComponent _playerPrefab;
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

    protected override void SolveDependencies()
    {
        // Solve private dependencies
    }

    public PlayerComponent GetPlayerPrefab()
    {
        return _playerPrefab;
    }
}