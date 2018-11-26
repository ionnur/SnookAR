using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForce : MonoBehaviour {

    Camera cam;

    public static int score = 0;

    public static int health = 100;

    public int healthValue;

    void Start()
    {
        //score = 0;
        //health = 100;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Goal")
        {
            //Debug.Log("Goal");
            Spawner.ballNumber--;
            health -= healthValue;
            Debug.Log("score: " + score);
            Debug.Log("ballNumber" + Spawner.ballNumber);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

        if (this.transform.position.y < 1.5)
        {
            //Debug.Log("fall");
            Spawner.ballNumber--;
            score++;
            Debug.Log("score: " + score);
            Debug.Log("ballNumber" + Spawner.ballNumber);
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            cam = Camera.allCameras[0];
            
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Debug.Log("click");

            RaycastHit hit;

            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 5.0f);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                Debug.Log("hit");

                if (hit.collider.gameObject == this.gameObject)
                {
                    this.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 1000.0f);
                }
            }
        }
	}
}