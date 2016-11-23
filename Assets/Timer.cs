using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
    public Text timer_text;
    public float TimeLimit;
    private float start_time;

	void Update () {
        TimeLimit -= Time.deltaTime;
        string minutes =(((int)TimeLimit / 60)).ToString();
        string seconds = ((TimeLimit % 60)).ToString("f0");

        timer_text.text = minutes + ":" + seconds;
        // add code to stop timer when you reach the end and win
	
	}
}
