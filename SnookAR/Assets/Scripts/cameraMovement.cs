using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {

    private const int MOVEMENT_SPEED = 3;

    // Update is called once per frame
    void Update () {

        //No movement
        Vector3 movement = new Vector3(0, 0, 0);

        //Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 1, 0);
        }

        //Right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -1, 0);
        }
    }
}
