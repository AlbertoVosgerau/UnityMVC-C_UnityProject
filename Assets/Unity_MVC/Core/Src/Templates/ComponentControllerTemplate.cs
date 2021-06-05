using UnityMVC;
public class ComponentControllerTemplate : ComponentController
{
    private ComponentTemplate _component;
    
    // Start your code here
    public void SetComponent(ComponentTemplate component)
    {
        _component = component;
    }
}