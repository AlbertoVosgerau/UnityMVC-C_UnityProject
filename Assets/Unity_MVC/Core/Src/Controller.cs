namespace UnityMVC
{
    public class Controller
    {
        public virtual void OnViewStart(){ }

        public virtual void OnViewDestroy()
        {
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }        
    }
}