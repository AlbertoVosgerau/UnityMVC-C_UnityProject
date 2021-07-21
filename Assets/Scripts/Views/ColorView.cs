using System;
using System.Collections.Generic;
using UnityEngine;
using UnityMVC;
using UnityMVC.View;

public class ColorViewEvents
{
    // Add events here
    public Action<View> onViewDestroyed;
}

public partial class ColorView
{
    private ColorController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ColorViewEvents Events => _events;
    private ColorViewEvents _events = new ColorViewEvents();

    private ColorMVCComponent _colorMvcComponent;
    private Renderer _renderer;
    protected override void SolveDependencies()
    {
        _colorMvcComponent = GetMVCComponent<ColorMVCComponent>();
        _renderer = _colorMvcComponent.GetUnityComponentFromMVC<Renderer>();
    }
    
    protected override void RegisterControllerEvents()
    {
        // otherObject.EventName += MyMethod;
    }

    protected override void UnregisterControllerEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }
}