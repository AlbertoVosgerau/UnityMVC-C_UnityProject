using UnityMVC;
public class ControllerTemplate : Controller
{
    public override void OnViewStart()
    {
        base.OnViewStart();   
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }
}
