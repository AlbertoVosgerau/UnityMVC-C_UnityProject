using UnityMVC;
public class CounterController : Controller
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
