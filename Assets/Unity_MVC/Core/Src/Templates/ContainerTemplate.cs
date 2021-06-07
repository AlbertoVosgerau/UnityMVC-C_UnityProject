using UnityMVC;
public class ContainerTemplate : Container
{
    protected LoaderTemplate Loader
    {
        get
        {
            if (_loader != null)
            {
                return _loader;
            }

            _loader = MVC.Loaders.Get<LoaderTemplate>();
            return _loader;
        }
    }
    protected LoaderTemplate _loader;
    
    // Start your code here
}