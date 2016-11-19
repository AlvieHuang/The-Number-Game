using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ResetData : MonoBehaviour {
    public void Start()
    {
        Reset();
    }

    //wipe the playerprefs data
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Reset!");
    }
}
