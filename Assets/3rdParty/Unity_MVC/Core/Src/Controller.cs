using System.Collections;

namespace UnityMVC
{
    public abstract class Controller
    {
        public abstract void SetView(View view);

        protected virtual void MVCOnInitializeController()
        {
            SolveDependencies();
        }

        public virtual void OnInitializeController()
        {
            MVCOnInitializeController();
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

        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
        protected abstract void SolveDependencies();
        public virtual void OnViewUpdate() {}

        protected virtual void MVCOnViewDestroy()
        {
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }
        public virtual void OnViewDestroy()
        {
            MVCOnViewDestroy();
        }        
    }
}