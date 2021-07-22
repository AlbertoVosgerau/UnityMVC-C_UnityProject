namespace UnityMVC.Model
{
    public abstract class Loader
    {
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
    }
}