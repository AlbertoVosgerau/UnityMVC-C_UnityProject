using UnityMVC;

public partial class SomeTestContainer : Container
{
    private SomeTestLoader Loader => _loader;
    public void Initialize()
    {
        if (_loader != null)
        {
            return;
        }

        _loader = MVCApplication.Loaders.Get<SomeTestLoader>();
    }
}
