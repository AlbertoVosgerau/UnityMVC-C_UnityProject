using UnityMVC;

public class ContainerTemplateEvents
{
    // Add events here
}

public partial class ContainerTemplate : Container
{
    protected LoaderTemplate Loader
    {
        get
        {
            if (_loader != null)
            {
                return _loader;
            }

            _loader = MVCApplication.Loaders.Get<LoaderTemplate>();
            return _loader;
        }
    }
    protected LoaderTemplate _loader;
    
    // Access Events from here. Please, use Observer pattern, people who uses Observer patterns are nice people.
    public ContainerTemplateEvents Events => _events;
    private ContainerTemplateEvents _events = new ContainerTemplateEvents();
}

public partial class ContainerTemplate
{
    // Start your code here
    protected override void UnregisterEvents()
    {
    }
    
    protected override void RegisterEvents()
    {
    }
}