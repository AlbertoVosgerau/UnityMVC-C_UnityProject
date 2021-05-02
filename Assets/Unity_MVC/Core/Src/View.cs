using UnityEngine;

namespace UnityMVC
{
    public class View : MonoBehaviour
    {
        private Controller _controller => MVC.Controllers.Get<Controller>();

        protected virtual void Awake()
        {
            _controller.SetView(this);
        }

        protected virtual void Start()
        {
            _controller.OnViewStart();
        }

        protected void OnDestroy()
        {
            _controller?.OnViewDestroy();
        }
    }
}