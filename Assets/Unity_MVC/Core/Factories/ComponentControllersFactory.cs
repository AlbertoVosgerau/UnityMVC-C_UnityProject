using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMVC;

public class ComponentControllersFactory
{
    public ComponentController Get<T>() where T: ComponentController
    {
        return new ComponentController() as T;
    }
}