using UnityEngine;
using System.Collections;

//single file for all utility extensions

public class Extensions : MonoBehaviour {
}

public static class TransformExtension
{

    public static void SetParent(this Transform transform, Transform parent, Vector3 newScale, bool worldPositionStays = false)
    {
        transform.SetParent(parent, worldPositionStays);
        transform.localScale = newScale;
    }

    public static void SetParentConstLocalScale(this Transform transform, Transform parent)
    {
        Vector3 localScale = transform.localScale;
        transform.SetParent(parent);
        transform.localScale = localScale;
    }
}

public static class VectorExtension
{
    public static Vector3 toWorldPoint(this Vector3 screenPoint)
    {
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public static Quaternion ToRotation(this Vector2 dir)
    {
        float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(_angle, Vector3.forward);
    }
}
