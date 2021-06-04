using UnityMVC;
public class ControllerTemplate : Controller
{
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