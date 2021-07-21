using System;
using UnityEngine;
using UnityMVC;
using UnityMVC.Controller;
using Random = UnityEngine.Random;

public class ColorControllerEvents
{
    // Add events here
    public Action<Controller> onControllerInitialized;
    public Action<Controller> onControllerDestroyed;
}
public partial class ColorController
{
    private ColorView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ColorControllerEvents Events => _events;
    private ColorControllerEvents _events = new ColorControllerEvents();

    private InputController _inputController;
    protected override void SolveDependencies()
    {
        _inputController = MVCApplication.Controllers.Get<InputController>();
    }
    protected override void RegisterEvents()
    {
        _inputController.Events.onColorChangeWasPressed += OnChangeColorWasPressed;
    }
    protected override void UnregisterEvents()
    {
        _inputController.Events.onColorChangeWasPressed += OnChangeColorWasPressed;
    }
    
    public override void OnInitializeController()
    {
        base.OnInitializeController();
        //Start your code from here
    }

    public override void OnViewStart()
    {
        base.OnViewStart();
        // Start your code from here
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
        // Start your code from here
    }

    private void OnChangeColorWasPressed()
    {
        _view.ChangeColor();
    }
}