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
            InternalAwake();
            RegisterEvents();
            MVCAwake();
        }
        public void OnViewStart()
        {
            CoroutineHelper.StartCoroutine(this,LateStartRoutine());
            MVCStart();
        }
        
        public void OnViewUpdate()
        {
            MVCUpdate();
        }
        
        public void OnViewOnEnable()
        {
            MVCOnEnable();
        }
        public void OnViewOnDisable()
        {
            MVCOnDisable();
        }
        public void OnViewDestroy()
        {
            UnregisterEvents();
            MVCOnDestroy();
            CoroutineHelper.StoppAllCoroutinesFromSender(this);
            InternalOnDestroy();
        }
        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            MVCLateStart();
        }
        
        protected virtual void MVCAwake() {}
        protected virtual void MVCStart() {}
        protected virtual void MVCUpdate() {}
        protected virtual void MVCOnEnable() {}
        protected virtual void MVCOnDisable() {}
        protected virtual void MVCOnDestroy() {}
        protected virtual void MVCLateStart()
        {
        }
    }
}