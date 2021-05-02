using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMVC;

public class TestView : View
{
    private TestController _controller => MVC.Controllers.Get<TestController>();

    private void Awake()
    {
        Debug.Log(_controller.TestString());
    }
}
