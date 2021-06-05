namespace UnityMVC
{
    public abstract class Controller
    {
        public abstract void SetView(View view);

        public virtual void OnViewStart(){}
        public virtual void OnViewUpdate() {}

        public virtual void OnViewDestroy()
        {
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }        
    }
}