namespace UnityMVC.Templates
{
    public class MyFancyCodeGeneratorView : View
    {
        private ControllerTemplate controller => MVC.Controllers.Get<ControllerTemplate>();

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}