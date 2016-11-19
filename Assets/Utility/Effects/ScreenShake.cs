using UnityEngine;
using System.Collections;


public class ScreenShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    public static IEnumerator RandomShakeRoutine(float duration, float magnitude)
    {
        while (duration > 0)
        {
            duration = duration - Time.deltaTime;
            Camera.main.transform.localPosition = Random.insideUnitCircle * magnitude;
            yield return new WaitForFixedUpdate();
        }
        Camera.main.transform.localPosition = Vector3.zero; //reset to default
    }

    public static Coroutine RandomShake(MonoBehaviour callingScript, float duration, float magnitude) //wrapper to include StartCoroutine
    {
        return callingScript.StartCoroutine(RandomShakeRoutine(duration, magnitude));
    }

    public static IEnumerator SmoothShakeRoutine(float duration, float magnitude, float frequency)
    {
        float time = 0;
        float randomX = Random.Range(-9999, 9999);
        float randomY = Random.Range(-9999, 9999);
        float transitionLength = duration > 2 * frequency ? frequency / 2 : duration / 2;
        while(time < transitionLength) //lerp into the shake
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Vector3.zero, magnitude * new Vector3(RangedPerlin(randomX, randomY + (frequency * duration)), RangedPerlin(randomX + (frequency * duration), randomY), RangedPerlin(randomX - (frequency * duration), randomY - (frequency * duration))), time / transitionLength);
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
        while (time < duration - transitionLength) //main period of shaking
        {
            Camera.main.transform.localPosition = magnitude * new Vector3(RangedPerlin(randomX, randomY + (frequency * duration)), RangedPerlin(randomX + (frequency * duration), randomY), RangedPerlin(randomX - (frequency * duration), randomY - (frequency * duration)));
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
        while (time < duration) // lerp out of the shake
        {
            Camera.main.transform.localPosition =Vector3.Lerp( magnitude * new Vector3(RangedPerlin(randomX, randomY + (frequency * duration)), RangedPerlin(randomX + (frequency * duration), randomY), RangedPerlin(randomX - (frequency * duration), randomY - (frequency * duration))), Vector3.zero, duration - time / transitionLength);
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
        Camera.main.transform.localPosition = Vector3.zero; //reset to default
    }

    private static float RangedPerlin(float x, float y) //maps perlin noise to [-1, 1] instead of [0, 1]
    {
        return (2 * Mathf.PerlinNoise(x, y)) - 1;
    }

    public static Coroutine SmoothShake(MonoBehaviour callingScript, float duration, float magnitude, float frequency) //wrapper to include StartCoroutine
    {
        return callingScript.StartCoroutine(SmoothShakeRoutine(duration, magnitude, frequency));
    }
}
