using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {

    public float move_speed;
	// Use this for initialization
	void Start () {
        move_speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(move_speed*Input.GetAxis("Horizontal")* Time.deltaTime, 0f, move_speed*Input.GetAxis("Vertical") * Time.deltaTime);
        
    }
}
