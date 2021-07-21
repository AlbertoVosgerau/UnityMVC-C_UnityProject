using System;
using UnityMVC.Controller;

public class InputControllerEvents
{
    // Add events here
    public Action<Controller> onControllerInitialized;
    public Action<Controller> onControllerDestroyed;

    public Action onColorChangeWasPressed;
}
public partial class InputController
{
    private InputView _view;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public InputControllerEvents Events => _events;
    private InputControllerEvents _events = new InputControllerEvents();
    
    // Start your code here
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    protected override void RegisterEvents()
    {
        _view.Events.changeColorWasPressed += OnColorChangeWasPressed;
    }
    protected override void UnregisterEvents()
    {
        _view.Events.changeColorWasPressed -= OnColorChangeWasPressed;
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

    private void OnColorChangeWasPressed()
    {
        _events.onColorChangeWasPressed?.Invoke();
    }
}