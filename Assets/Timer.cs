using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
    public Text timer_text;
    private float start_time;
	// Use this for initialization
	void Start () {
        start_time = Time.time;


    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - 600;

        string minutes =(-((int)t / 60)).ToString();
        string seconds = (-(t % 60)).ToString("f0");

        timer_text.text = minutes + ":" + seconds;
        // add code to stop timer when you reach the end and win
	
	}
}
