using UnityEngine;
using System.Collections;

//basically the same as Invoke() and InvokeRepeating(), but no reflection!

//probably will need expansions/reworks when I actually use this in something


public static class Callback {
    public delegate void CallbackMethod();

    //code that accepts a lerp value from zero to one
    public delegate void Lerpable(float lerpValue);

    //keep the autocomplete namespace distinct between the IEnumerators and the coroutines
    public static class Routines
    {
        //basically Invoke
        public static IEnumerator FireAndForgetRoutine(CallbackMethod code, float time)
        {
            yield return new WaitForSeconds(time);
            code();
        }

        //Fires the code on the next fixed update. Lazy way to keep the code from affecting what you're doing right now
        public static IEnumerator FireForFixedUpdateRoutine(CallbackMethod code)
        {
            yield return new WaitForFixedUpdate();
            code();
        }

        // same, but for the normal update instead of fixed update
        public static IEnumerator FireForNextFrameRoutine(CallbackMethod code)
        {
            yield return 0;
            code();
        }

        //same as WaitForSeconds(), but is not affected by timewarping
        public static IEnumerator WaitForRealSecondsRoutine(float seconds)
        {
            float pauseStartTime = Time.realtimeSinceStartup;
            float pauseEndTime = pauseStartTime + seconds;

            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
        }

        //does a standard coroutine Lerp on a bit of code.
        public static IEnumerator DoLerpRoutine(Lerpable code, float time, bool reverse = false)
        {
            if (!reverse)
            {
                float timeElapsed = 0;
                while (timeElapsed < time)
                {
                    code(timeElapsed / time);
                    yield return null;
                    timeElapsed += Time.deltaTime;
                }
            }
            else
            {
                float timeRemaining = time;
                while (timeRemaining > 0)
                {
                    code(timeRemaining / time);
                    yield return null;
                    timeRemaining -= Time.deltaTime;
                }
            }
            code(1);
        }

        //same, but run the lerp code independent of any timewarping
        public static IEnumerator DoLerpRealtimeRoutine(Lerpable code, float lerpTime, bool reverse = false)
        {
            float realStartTime = Time.realtimeSinceStartup;
            float realEndTime = realStartTime + lerpTime;
            if (!reverse)
            {
                while (Time.realtimeSinceStartup < realEndTime)
                {
                    code((Time.realtimeSinceStartup - realStartTime) / lerpTime);
                    yield return null;
                }
            }
            else
            {
                while (Time.realtimeSinceStartup < realEndTime)
                {
                    code((realEndTime - Time.realtimeSinceStartup) / lerpTime);
                    yield return null;
                }
            }

            code(1);
        }

        //used Time.FixedDeltaTime instead of delta time (for important physics/gameplay things)
        public static IEnumerator DoLerpFixedtimeRoutine(Lerpable code, float time, bool reverse = false)
        {
            if (!reverse)
            {
                float timeElapsed = 0;
                while (timeElapsed < time)
                {
                    code(timeElapsed / time);
                    yield return new WaitForFixedUpdate();
                    timeElapsed += Time.fixedDeltaTime;
                }
            }
            else
            {
                float timeRemaining = time;
                while (timeRemaining > 0)
                {
                    code(timeRemaining / time);
                    yield return new WaitForFixedUpdate();
                    timeRemaining -= Time.fixedDeltaTime;
                }
            }

            code(1);
        }
    }

    private static Coroutine RunIfActiveAndEnabled(MonoBehaviour callingScript, IEnumerator code)
    {
        if (callingScript.isActiveAndEnabled)
            return callingScript.StartCoroutine(code);
        else
            return null;
    }

    //wrappers for the routines in the Routines class so that we don't need to call StartCoroutine()
    public static Coroutine FireAndForget(this CallbackMethod code, float time, MonoBehaviour callingScript)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.FireAndForgetRoutine(code, time));
    }
    public static Coroutine FireForFixedUpdate(this CallbackMethod code, MonoBehaviour callingScript)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.FireForFixedUpdateRoutine(code));
    }

    public static Coroutine FireForNextFrame(this CallbackMethod code, MonoBehaviour callingScript)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.FireForNextFrameRoutine(code));
    }

    public static Coroutine WaitForRealSeconds(float seconds, MonoBehaviour callingScript)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.WaitForRealSecondsRoutine(seconds));
    }

    public static Coroutine DoLerp(Lerpable code, float time, MonoBehaviour callingScript, bool reverse = false)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.DoLerpRoutine(code, time, reverse));
    }

    public static Coroutine DoLerpRealtime(Lerpable code, float time, MonoBehaviour callingScript, bool reverse = false)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.DoLerpRealtimeRoutine(code, time, reverse));
    }

    public static Coroutine DoLerpFixedtime(Lerpable code, float time, MonoBehaviour callingScript, bool reverse = false)
    {
        return RunIfActiveAndEnabled(callingScript, Routines.DoLerpFixedtimeRoutine(code, time, reverse));
    }
}
