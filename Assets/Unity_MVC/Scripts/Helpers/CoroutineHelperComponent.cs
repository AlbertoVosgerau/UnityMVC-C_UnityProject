using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelperComponent : MonoBehaviour
{
    public static CoroutineHelperComponent Instance => _instance;
    private static CoroutineHelperComponent _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(gameObject);
    }
}
