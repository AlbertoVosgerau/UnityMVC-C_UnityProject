using System.Collections;

namespace UnityMVC
{
    public abstract class Controller
    {
        public abstract void SetView(View view);

        public virtual void OnInitializeController()
        {
            SolveDependencies();
        }

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStart();
        }

        public virtual void OnViewStart()
        {
            CoroutineHelper.StartCoroutine(this,LateStartRoutine());
        }

        protected virtual void LateStart()
        {
        }

        protected abstract void SolveDependencies();
        public virtual void OnViewUpdate() {}
        public virtual void OnViewDestroy()
        {
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }        
    }
}