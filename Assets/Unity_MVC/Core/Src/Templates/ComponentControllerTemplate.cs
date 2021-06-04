using UnityMVC;
public class ComponentControllerTemplate : ComponentController
{
    private ComponentTemplate _component;

    public void SetComponent(ComponentTemplate component)
    {
        _component = component;
    }
}