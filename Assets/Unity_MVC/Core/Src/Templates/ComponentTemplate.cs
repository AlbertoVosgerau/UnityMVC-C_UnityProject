using UnityMVC;

public class ComponentTemplateEvents
{
    // Add your actions and events here
}

public class ComponentTemplateInfo
{
    // Add metadata here
}

public class ComponentTemplate : Component
{
    public ComponentTemplateInfo Info => _info;
    private ComponentTemplateInfo _info = new ComponentTemplateInfo();
    public ComponentTemplateEvents Events => _events;
    private ComponentTemplateEvents _events = new ComponentTemplateEvents();

    // Start your code here
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SolveDependencies()
    {
        // Solve private dependencies
    }
}