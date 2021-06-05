using System;
using UnityEngine;
using Component = UnityMVC.Component;

public class PlayerComponent : Component
{
    // TODO: Create a system to get the controller just passing the component instance to MVC
    public PlayerComponentController Controller => _controller;
    private PlayerComponentController _controller;
    
    // Start your code here
    protected override void Awake()
    {
        _controller = new PlayerComponentController();
        _controller.SetComponent(this);
        base.Awake();
        _controller.OnComponentAwake();
    }
    protected virtual void Start()
    {
        _controller.OnComponentStart();
    }

    protected virtual void Update()
    {
        _controller.OnComponentUpdate();
    }

    protected void OnDestroy()
    {
        _controller.OnComponentDestroy();
    }

    protected override void SolveDependencies()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        _controller.OnBallHit(other);
    }
}