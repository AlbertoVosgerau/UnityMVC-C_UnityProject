using System.Collections;
using UnityEngine;

namespace UnityMVC
{
    public abstract class View : MonoBehaviour
    {
        protected abstract void LocateController();

        protected virtual void Awake()
        {
            LocateController();
            SolveDependencies();
        }

        protected virtual void Start()
        {
            RegisterControllerEvents();
            StartCoroutine(LateStartRoutine());
        }

        protected IEnumerator LateStartRoutine()
        {
            yield return null;
            LateStart();
        }

        protected virtual void LateStart()
        {
        }

        protected virtual void OnDestroy()
        {
            UnregisterControllerEvents();
        }

        protected virtual void SolveDependencies(){}

        protected abstract void RegisterControllerEvents();

        protected abstract void UnregisterControllerEvents();
    }
}