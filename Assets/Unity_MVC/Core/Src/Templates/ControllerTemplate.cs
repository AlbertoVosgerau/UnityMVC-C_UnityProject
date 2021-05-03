using UnityMVC;

public class ControllerTemplate : Controller
{
    private View _view;

    public override void OnViewStart()
    {
        base.OnViewStart();   
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }
}
