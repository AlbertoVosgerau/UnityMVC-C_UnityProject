using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelperComponent : MonoBehaviour
{
    public static CoroutineHelperComponent Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newObject = new GameObject("CoroutineHelper");
                _instance = newObject.AddComponent<CoroutineHelperComponent>();
                DontDestroyOnLoad(newObject);
            }
            return _instance;
        }
    }
    private static CoroutineHelperComponent _instance;
}
