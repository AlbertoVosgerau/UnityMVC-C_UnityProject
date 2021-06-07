using System;
using System.Collections;
using UnityEngine;

namespace UnityMVC
{
    public class Component : MonoBehaviour
    {
        protected View _view;
        public void SetView(View view)
        {
            _view = view;
        }

        protected virtual void Awake()
        {
            SolveDependencies();
        }

        protected virtual void Start()
        {
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

        protected virtual void SolveDependencies()
        {
            
        }
    }
}