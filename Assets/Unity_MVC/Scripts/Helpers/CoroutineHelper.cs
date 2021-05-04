using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper
{
    public static void StartCoroutine(IEnumerator iEnumerator)
    {
        if (CoroutineHelperComponent.Instance == null)
        {
            GameObject newObject = new GameObject("CoroutineHelper", typeof(CoroutineHelperComponent));
        }

        Coroutine coroutine = CoroutineHelperComponent.Instance.StartCoroutine(iEnumerator);
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (CoroutineHelperComponent.Instance == null)
        {
            return;
        }
        
        CoroutineHelperComponent.Instance.StopCoroutine(coroutine);
    }

    public static void StopAllCoroutines()
    {
        if (CoroutineHelperComponent.Instance == null)
        {
            return;
        }
        
        CoroutineHelperComponent.Instance.StopAllCoroutines();
    }
}
