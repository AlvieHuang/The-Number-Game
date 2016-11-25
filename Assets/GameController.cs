using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform pauseUI;
    [SerializeField] private UnityEngine.UI.Text timer_text;
    [SerializeField] private float TimeLimit;
    private float start_time;
    //private bool paused;

    // Use this for initialization
    void Start()
    {
        //FirstPersonController script = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //--------------Timer
        TimeLimit -= Time.deltaTime;
        string minutes = (((int)TimeLimit / 60)).ToString();
        string seconds = ((TimeLimit % 60)).ToString("f0");

        timer_text.text = minutes + ":" + seconds;
        // add code to stop timer when you reach the end and win

        //-----------Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.gameObject.activeInHierarchy == false)
            {
                pauseUI.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (pauseUI.gameObject.activeInHierarchy == true)
            {
                pauseUI.gameObject.SetActive(false);
            }
        }

    }
}
