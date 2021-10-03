#if UNITY_EDITOR
using UnityMVC.Component;

namespace UnityMVC.Inspector
{
    public class MVCDependencyResult
    {
        public bool IsOk => controllers.IsOk && views.IsOk && mvcComponents.IsOk && mvcComponentGroups.IsOk && unityComponents.IsOk;
        
        public MVCInspectorData<Controller.Controller> controllers = new MVCInspectorData<Controller.Controller>();
        public MVCInspectorData<View.View> views = new MVCInspectorData<View.View>();
        public MVCInspectorData<MVCComponent> mvcComponents = new MVCInspectorData<MVCComponent>();
        public MVCInspectorData<MVCComponentGroup> mvcComponentGroups = new MVCInspectorData<MVCComponentGroup>();
        public MVCInspectorData<UnityComponent.UnityComponent> unityComponents = new MVCInspectorData<UnityComponent.UnityComponent>();
    }
}
#endif