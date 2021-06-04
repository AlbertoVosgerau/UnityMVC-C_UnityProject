using System;
using UnityMVC;
public class ComponentTemplate : Component
{
    private ComponentControllerTemplate _controller;
    protected override void Awake()
    {
        _controller = MVC.ComponentControllerFactory.Get<ComponentControllerTemplate>() as ComponentControllerTemplate;
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
}