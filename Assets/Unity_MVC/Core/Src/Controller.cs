using System.Collections;

namespace UnityMVC
{
    public abstract class Controller
    {
        public abstract void SetView(View view);

        public virtual void OnViewStart()
        {
            SolveDependencies();
            CoroutineHelper.StartCoroutine(this,LateStartRoutine());
        }

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStart();
        }

        protected virtual void LateStart()
        {
        }

        protected virtual void SolveDependencies(){}
        public virtual void OnViewUpdate() {}
        public virtual void OnViewDestroy()
        {
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }        
    }
}