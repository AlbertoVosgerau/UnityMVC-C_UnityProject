using System;
using UnityEngine;

public class ColorMVCComponentEvents
{
    // Add your actions and events here
    public Action onCreated;
    public Action onDestroyed;
}

public partial class ColorMVCComponent
{
    private ViewTemplate _view => _baseView as ViewTemplate;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ColorMVCComponentEvents Events => _events;
    private ColorMVCComponentEvents _events = new ColorMVCComponentEvents();
    
    // Start your code here
    private Renderer _renderer;
    private MaterialColorRandomizer _colorRandomizer;
    
    protected override void SolveDependencies()
    {
        _renderer = GetUnityComponentFromMVC<Renderer>(true);
        _colorRandomizer = GetUnityComponentFromMVC<MaterialColorRandomizer>(true);
    }
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }

    protected override void Awake()
    {
        base.Awake();
        // Add your code from here
    }
    
    protected override void Start()
    {
        base.Start();
        // Add your code from here
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // Add your code from here
    }

    public void SetRandomColorToMaterial()
    {
        _colorRandomizer.ChangeColor(_renderer.material);
    }
}