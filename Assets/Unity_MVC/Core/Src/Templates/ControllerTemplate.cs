using UnityMVC;
public class ControllerTemplate : Controller
{
    private ViewTemplate _view;
    public override void SetView(View view)
    {
        _view = view as ViewTemplate;
    }
    
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