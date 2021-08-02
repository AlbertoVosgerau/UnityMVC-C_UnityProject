using UnityMVC.Events;

namespace UnityMVC.Events
{
    public partial class ControllerTemplateEvents
    {
        // Add your actions and events here
    }
}

namespace UnityMVC.Model
{
    public class MVCComponentTemplateModel : MVCModel
    {
        // Add data here
    }
}

public partial class MVCComponentTemplate
{
    //// MVC properties available: View, Events and Data

    // Start your code here
    protected override void SolveDependencies()
    {
        // Awake calls this method. Solve your dependencies here.
    }
    
    protected override void RegisterEvents()
    {
        // otherObject.EventName += MyMethod;
    }
    
    protected override void UnregisterEvents()
    {
        // otherObject.EventName -= MyMethod;
    }
    
    protected override void MVCAwake()
    {
        // Add your code here
    }

    protected override void MVCStart()
    {
        // Add your code here
    }

    protected override void MVCLateStart()
    {
        // Add your code here
    }

    protected override void MVCUpdate()
    {
        // Add your code here
    }
}