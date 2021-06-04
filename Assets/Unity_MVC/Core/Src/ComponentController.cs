
namespace UnityMVC
{
    public class ComponentController
    {
        public virtual void OnComponentAwake(){}
        public virtual void OnComponentStart(){}
        public virtual void OnComponentUpdate() {}
        public virtual void OnComponentFixedUpdate(){}
        public virtual void OnComponentLateUpdate(){}
        public virtual void OnComponentEnable(){}
        public virtual void OnComponentDisable(){}
        public virtual void OnComponentDestroy(){}
    }
}