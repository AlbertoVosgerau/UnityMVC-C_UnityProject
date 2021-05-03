using UnityEngine;

namespace UnityMVC
{
    public class View : MonoBehaviour
    {
        private Controller controller => MVC.Controllers.Get<Controller>();

        protected virtual void Awake()
        {
            
        }

        protected virtual void RegisterControllerEvents()
        {
            
        }

        protected virtual void UnregisterControllerEvents()
        {
            
        }

        protected virtual void Start()
        {
            controller.OnViewStart();
        }

        protected virtual void OnDestroy()
        {
            controller?.OnViewDestroy();
        }
    }
}