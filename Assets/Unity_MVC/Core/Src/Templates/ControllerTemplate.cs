using UnityMVC;

public class ControllerTemplateEvents
{
    // Add events here
}

public class ControllerTemplate : Controller
{
    private ViewTemplate _view;
    public override void SetView(View view)
    {
        _view = view as ViewTemplate;
    }
    
    public ControllerTemplateEvents Events => _events;
    private ControllerTemplateEvents _events = new ControllerTemplateEvents();
    
    // Start your code here

    public override void OnViewStart()
    {
        base.OnViewStart();   
    }

    public override void OnViewUpdate()
    {
        base.OnViewUpdate();
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }
}