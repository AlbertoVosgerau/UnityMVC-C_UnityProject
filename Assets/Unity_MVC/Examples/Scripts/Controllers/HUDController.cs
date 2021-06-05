using UnityMVC;
public class HUDController : Controller
{
    private HUDView _view;
    public override void SetView(View view)
    {
        _view = view as HUDView;
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