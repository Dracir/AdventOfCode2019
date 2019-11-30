using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class UnityObjectExtensions
{
    public static void Destroy(this UnityEngine.Object obj, bool allowDestroyingAssets = false)
    {
        if (Application.isPlaying)
            UnityEngine.Object.Destroy(obj);
        else
            UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets);
    }
}
