using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    public float move_speed;
    public float rotate_speed;
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
        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
        }
        //rbody.AddForce(moveX, 0f, moveZ); /* for collisions, don't use*/
    }
}
