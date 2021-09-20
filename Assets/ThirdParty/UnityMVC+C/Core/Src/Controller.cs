using System.Collections;
using UnityEngine;

namespace UnityMVC.Controller
{
    public abstract class Controller
    {
        internal abstract void SetView(View.View view);
        public abstract View.View GetView();
        public abstract bool IsActive();
        protected void DontDestroyOnLoad(View.View view)
        {
            view.transform.parent = null;
            Object.DontDestroyOnLoad(view);
        }

        protected abstract void InternalAwake();
        protected abstract void InternalOnDestroy();
        protected abstract void InternalOnEnable();
        protected abstract void InternalOnDisable();
        
        protected abstract void SolveDependencies();
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        internal void OnViewAwake()
        {
            SolveDependencies();
            InternalAwake();
            RegisterEvents();
            MVCAwake();
        }
        internal void OnViewStart()
        {
            CoroutineHelper.StartCoroutine(this,LateStartRoutine());
            MVCStart();
        }
        
        internal void OnViewUpdate()
        {
            MVCUpdate();
        }
        
        internal void OnViewOnEnable()
        {
            InternalOnEnable();
            MVCOnEnable();
        }
        internal void OnViewOnDisable()
        {
            InternalOnDisable();
            MVCOnDisable();
        }
        internal void OnViewDestroy()
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