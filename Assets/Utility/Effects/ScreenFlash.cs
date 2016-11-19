using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//should be placed on some sort of UI image which covers the entire screen

public class ScreenFlash : MonoBehaviour {
    CanvasGroup group;
    IEnumerator flash;
    void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }
    private IEnumerator FlashRoutine(float durationRealSeconds, float startAlpha = 0.5f)
    {
        //this effect is likely to be called alongside time-warp effects, so we'll use real time
        yield return Callback.DoLerpRealtime((float x) => { group.alpha = (Mathf.Lerp(startAlpha, 0, (x))); }, durationRealSeconds, this);
        flash = null;
    }

    public Coroutine Flash(float durationRealSeconds, float startAlpha = 0.5f)
    {
        if(flash != null)
            StopCoroutine(flash);
        flash = FlashRoutine(durationRealSeconds, startAlpha);
        return StartCoroutine(flash);
    }

    IEnumerator FadeRoutine(float duration)
    {
        float time = 0;
        while (time < duration - 0.25f)
        {
            group.alpha = time / (duration - 0.25f);
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }
        group.alpha = 1;
        yield return new WaitForSeconds(0.25f);
        group.alpha = 0;
    }

    public Coroutine Fade(float duration)
    {
        return StartCoroutine(FadeRoutine(duration));
    }
}
