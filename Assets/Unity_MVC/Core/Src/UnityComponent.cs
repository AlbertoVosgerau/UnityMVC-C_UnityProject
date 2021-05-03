using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMVC;

public class UnityComponent : MonoBehaviour
{
    protected UnityView _view;
    
    public void SetView(UnityView view)
    {
        _view = view;
    }
}
