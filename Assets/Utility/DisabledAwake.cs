using UnityEngine;
using System.Collections;

public class DisabledAwake : MonoBehaviour {

    void Start()
    {
        foreach (IDisabledStart sleeper in GetComponentsInChildren<IDisabledStart>(includeInactive : true))
            sleeper.StartDisabled();
    }

    void Awake()
    {
        foreach (IDisabledAwake sleeper in GetComponentsInChildren<IDisabledAwake>(includeInactive: true))
            sleeper.Awaken();
    }
}

//can't use awake and start because those will also get called when the objects are activated

public interface IDisabledAwake
{
    void Awaken();
}

public interface IDisabledStart
{
    void StartDisabled();
}
