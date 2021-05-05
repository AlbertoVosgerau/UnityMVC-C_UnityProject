using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC
{
    public class CoroutineHelper
    {
        private static Dictionary<System.Object, Coroutine> _coroutines;
        public static void StartCoroutine(System.Object sender, IEnumerator iEnumerator)
        {
            if (CoroutineHelperComponent.Instance == null)
            {
                GameObject newObject = new GameObject("CoroutineHelper", typeof(CoroutineHelperComponent));
                StopAllCoroutines();
            }

            Coroutine coroutine = CoroutineHelperComponent.Instance.StartCoroutine(iEnumerator);
            _coroutines.Add(sender, coroutine);
        }
    
        public static void StopCoroutine(Coroutine coroutine)
        {
            if (CoroutineHelperComponent.Instance == null)
            {
                return;
            }
        
            CoroutineHelperComponent.Instance.StopCoroutine(coroutine);
            foreach (KeyValuePair<System.Object,Coroutine> kv in _coroutines)
            {
                if (kv.Value == coroutine)
                {
                    _coroutines.Remove(kv.Key);
                }
            }
        }

        public static void StoppAllCoroutinesFromSender(System.Object sender)
        {
            if (CoroutineHelperComponent.Instance == null)
            {
                return;
            }

            foreach (KeyValuePair<System.Object, Coroutine> kv in _coroutines)
            {
                if (kv.Key == sender)
                {
                    CoroutineHelperComponent.Instance.StopCoroutine(kv.Value);
                    _coroutines.Remove(sender);
                }
            }
        }

        public static void StopAllCoroutines()
        {
            if (CoroutineHelperComponent.Instance == null)
            {
                return;
            }
        
            CoroutineHelperComponent.Instance.StopAllCoroutines();
            _coroutines.Clear();
        }
    }
}