using UnityEngine;

namespace UnityMVC
{
    public class UnityView : MonoBehaviour
    {
        private UnityController controller => MVC.Controllers.Get<UnityController>();

        protected virtual void Awake()
        {
            controller.SetView(this);
        }

        protected virtual void Start()
        {
            controller.OnViewStart();
        }

        protected void OnDestroy()
        {
            controller?.OnViewDestroy();
        }
    }
}