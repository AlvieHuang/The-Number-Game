using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DespawnOnAudioFinish : MonoBehaviour {

    AudioSource sound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
	// Use this for initialization
    void OnEnable()
    {
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine()
    {
        yield return new WaitForSeconds(sound.clip.length);
        SimplePool.Despawn(this.gameObject);
    }
}
