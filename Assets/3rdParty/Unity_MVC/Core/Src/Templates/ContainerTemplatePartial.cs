namespace UnityMVC.Model
{
    public partial class ContainerTemplate : Container
    {
        private LoaderTemplate Loader => _loader;
        public void Initialize()
        {
            if (_loader != null)
            {
                return;
            }

            _loader = MVCApplication.Loaders.Get<LoaderTemplate>();
        }
    }
}