using System.Collections;
using UnityEngine;

namespace UnityMVC.Controller
{
    public abstract class Controller
    {
        public abstract void SetView(View.View view);
        protected void DontDestroyOnLoad(View.View view)
        {
            view.transform.parent = null;
            Object.DontDestroyOnLoad(view);
        }

        protected abstract void InternalAwake();
        protected abstract void InternalOnDestroy();
        
        protected abstract void SolveDependencies();
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        public void OnViewAwake()
        {
            SolveDependencies();
            AwakeMVC();
        }
        public void OnViewStart()
        {
            CoroutineHelper.StartCoroutine(this,LateStartRoutine());
            StartMVC();
        }
        public virtual void OnViewUpdate()
        {
            UpdateMVC();
        }
        public void OnViewOnEnable()
        {
            OnEnableMVC();
        }
        public void OnViewOnDisable()
        {
            OnDisableMVC();
        }
        public void OnViewDestroy()
        {
            OnDestroyMVC();
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
        }
        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStartMVC();
        }
        
        protected virtual void AwakeMVC() {}
        protected virtual void StartMVC() {}
        protected virtual void UpdateMVC() {}
        protected virtual void OnEnableMVC() {}
        protected virtual void OnDisableMVC() {}
        protected virtual void OnDestroyMVC() {}
        protected virtual void LateStartMVC()
        {
        }
    }
}