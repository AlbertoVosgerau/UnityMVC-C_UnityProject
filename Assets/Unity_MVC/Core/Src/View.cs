using UnityEngine;

namespace UnityMVC
{
    public abstract class View : MonoBehaviour
    {
        protected abstract void LocateController();

        protected virtual void Awake()
        {
            LocateController();
        }

        protected abstract void RegisterControllerEvents();

        protected abstract void UnregisterControllerEvents();
    }
}