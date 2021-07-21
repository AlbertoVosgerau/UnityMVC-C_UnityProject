using System;
using Implementations;
using UnityMVC.View;

public class InputViewEvents
{
    // Add events here
    public Action<View> onViewDestroyed;

    public Action changeColorWasPressed;
}

public partial class InputView
{
    private InputController _controller;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public InputViewEvents Events => _events;
    private InputViewEvents _events = new InputViewEvents();
    
    // Start your code here

    private InputImpl _input;
    
    protected override void SolveDependencies()
    {
        _input = new InputImpl();
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
        if (_input.ChangeColor)
        {
            _events.changeColorWasPressed?.Invoke();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}