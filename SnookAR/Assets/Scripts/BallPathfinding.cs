﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallPathfinding : MonoBehaviour {

	/* BallPathfinding CLASS
	 * 
	 * Follows a preset path (and returns to it
	 * from the point it left) while also
	 * allowing the player to interact with
	 * the ball's position.
	 * 
	 * -Kieran
	 * */

	//Holds all the pathfinding vectors.
	private PathfindingMaster master;

	//The next point the ball is moving toward
	//(in terms of master's point array).
	private int pathfindingIndex = 0;

	private Rigidbody rb;
	private Vector2 flatPos;


    public bool currentlyHit;
    public Vector3 velocity;
    public float magnitude;
    //How much force is applied to the ball
    //each frame to "steer" it.
    [SerializeField]

	private float force = 1;
    private float weight = 1;

    void Start() {
		master = FindObjectOfType<PathfindingMaster>();
		rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 30, ForceMode.Impulse);
        //Time.timeScale = 0.1f;
	}

	void FixedUpdate() {

        velocity = rb.velocity;
        magnitude = rb.velocity.magnitude;

		//Check for changes in pathfinding node.
		flatPos = new Vector2(transform.position.x, transform.position.z);

		if((master.path[pathfindingIndex] - flatPos).sqrMagnitude < 3) {
			//Move to the next pathfinding index.
			if(pathfindingIndex < master.path.Length - 1) pathfindingIndex++;
			else {
				//Do something important involving
				//points etc.
			};
		}

        //If the ball has been hit with force.
        if (velocity.magnitude > 3f)
        {
            currentlyHit = true;
        }
        //When the ball comes to a stop after being hit
        if (velocity.magnitude < 0.1f)
        {
            currentlyHit = false;
        }

        //If the ball hasn't been hit, perform normal pathfinding actions.
        if (currentlyHit == false)
        {
            //Move towards the next node.
            rb.AddForce(new Vector3(master.path[pathfindingIndex].x - flatPos.x, 0, master.path[pathfindingIndex].y - flatPos.y).normalized * force);
        }


		Debug.Log(master.path[pathfindingIndex] + " ... " + flatPos);

	}
}
