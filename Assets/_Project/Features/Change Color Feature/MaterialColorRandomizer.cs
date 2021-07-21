using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorRandomizer : MonoBehaviour
{
    public void ChangeColor(Material material)
    {
        material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
