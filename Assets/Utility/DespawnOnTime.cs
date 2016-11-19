using UnityEngine;
using System.Collections;

public class DespawnOnTime : MonoBehaviour {

    public float time;

    void OnEnable()
    {
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine()
    {
        yield return new WaitForSeconds(time);
        SimplePool.Despawn(this.gameObject);
    }
}
