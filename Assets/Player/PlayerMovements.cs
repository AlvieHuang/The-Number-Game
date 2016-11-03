using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    public float move_speed;
    public float rotate_speed;

    void Rotate()
    {
        var rot = new Vector3(0f, 0f, 0f);
        // rotates Camera Left
        if (Input.GetKey(KeyCode.Q))
        {
            rot.y -= 1;
        }
        // rotates Camera Left
        if (Input.GetKey(KeyCode.E))
        {
            rot.y += 1;
        }


        transform.Rotate(rot, rotate_speed * Time.deltaTime);
    }

    void Update()
    {
        Rotate();
        transform.Translate(move_speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, move_speed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
