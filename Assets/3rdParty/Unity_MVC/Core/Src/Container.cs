namespace UnityMVC.Model
{
    public abstract class Container
    {
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
    }
}