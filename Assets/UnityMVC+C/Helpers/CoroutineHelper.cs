using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC
{
    public class CoroutineHelper
    {
        private static List<(object sender, Coroutine coroutine)> _coroutines = new List<(object sender, Coroutine coroutine)>();
        private static List<Coroutine> _dontDestroyCoroutines = new List<Coroutine>(); 
        public static void StartCoroutine(System.Object sender, IEnumerator iEnumerator, bool dontDestroyOnLoad = false)
        {
            Coroutine coroutine = CoroutineHelperComponent.Instance.StartCoroutine(iEnumerator);
            _coroutines.Add((sender, coroutine));
            if (dontDestroyOnLoad)
            {
                if (!_dontDestroyCoroutines.Contains(coroutine))
                {
                    _dontDestroyCoroutines.Add(coroutine);
                }
            }
        }
    
        public static void StopCoroutine(Coroutine coroutine)
        {
            CoroutineHelperComponent.Instance.StopCoroutine(coroutine);

            foreach ((object, Coroutine) tuple in _coroutines)
            {
                _coroutines.Remove(tuple);
                RemoveFromDontDestroyOnLoad(tuple.Item2);
            }
        }

        public static void StoppAllCoroutinesFromSender(object sender)
        {
            for (int i = 0; i < _coroutines.Count; i++)
            {
                (object, Coroutine) tuple = _coroutines[i];
                if (tuple.Item1 == sender)
                {
                    CoroutineHelperComponent.Instance.StopCoroutine(_coroutines[i].Item2);
                    _coroutines.Remove(tuple);
                    RemoveFromDontDestroyOnLoad(tuple.Item2);
                }
            }
        }

        public static void StopAllCoroutines()
        {
            CoroutineHelperComponent.Instance.StopAllCoroutines();
            _coroutines.Clear();
        }

        private static void RemoveFromDontDestroyOnLoad(Coroutine coroutine)
        {
            if (_dontDestroyCoroutines.Contains(coroutine))
            {
                _dontDestroyCoroutines.Remove(coroutine);
            }
        }
    }
}