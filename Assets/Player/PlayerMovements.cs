using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    public float move_speed = 8;
    public float rotate_speed = 100;
    public bool toggle = true;
    //private Rigidbody rbody; /* for collisions, don't use*/

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //rbody = GetComponent<Rigidbody>(); /* for collisions, don't use*/
    }

    void Update()
    {
        float moveX = move_speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveZ = move_speed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.Translate(moveX, 0f, moveZ);
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (toggle)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            toggle = !toggle;
        }
        //rbody.AddForce(moveX, 0f, moveZ); /* for collisions, don't use*/
    }
}
