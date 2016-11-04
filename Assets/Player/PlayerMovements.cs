using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    public float move_speed;
    public float rotate_speed;
    //private Rigidbody rbody; /* for collisions, don't use*/

    void Start()
    {
        //rbody = GetComponent<Rigidbody>(); /* for collisions, don't use*/
    }
    void Rotate()
    {
        var rot = new Vector3(0f, 0f, 0f);
        // rotates Camera Left
        if (Input.GetAxis("Mouse X") < 0)
        {
            rot.y -= 1;
        }
         // rotates Camera Right
        if (Input.GetAxis("Mouse X") > 0)
        {
            rot.y += 1;
        }


        transform.Rotate(rot, rotate_speed * Time.deltaTime);
    }

    void Update()
    {
        float moveX = move_speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveZ = move_speed * Input.GetAxis("Vertical") * Time.deltaTime;
        Rotate();
        transform.Translate(moveX, 0f, moveZ);
        //rbody.AddForce(moveX, 0f, moveZ); /* for collisions, don't use*/
    }
}
