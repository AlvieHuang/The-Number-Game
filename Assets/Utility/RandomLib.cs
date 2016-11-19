using UnityEngine;
using System.Collections;

public static class RandomLib {

    public static float RandFloatRange(float midpoint, float variance)
    {
        return midpoint + (variance * Random.value);
    }
}
