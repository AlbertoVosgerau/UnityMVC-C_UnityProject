using UnityMVC;
public class TestController : Controller
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
