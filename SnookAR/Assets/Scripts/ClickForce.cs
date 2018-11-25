using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForce : MonoBehaviour {

    Camera cam;

    public static int score = 0;

    public static int health = 100;

    public int healthValue;

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
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                if (hit.collider.gameObject == this.gameObject)
                {
                    this.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 1000.0f);
                }
            }
        }
	}
}