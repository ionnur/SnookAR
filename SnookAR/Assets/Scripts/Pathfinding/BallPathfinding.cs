using System.Collections;
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
	//(in terms of master's path).
	private int pathfindingIndex = 0;

	//The "emergency backup" path the ball will
	//use to get back to the path.
	private List<Vector3> path;

	private bool useDefaultPath = true;

	private Rigidbody rb;


    public static bool currentlyHit;

    //How much force is applied to the ball
    //each frame to "steer" it.
    [SerializeField]

	private float force = 1;
    private float weight = 1;


    void Start() {
		master = FindObjectOfType<PathfindingMaster>();
		rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.right * 30, ForceMode.Impulse);
        currentlyHit = false;
	}

	void FixedUpdate() {
		//Check for changes in pathfinding node.
		if(useDefaultPath) {
			if((master.mainPath[pathfindingIndex] - transform.position).sqrMagnitude < 1) {
				useDefaultPath = true;
				//Move to the next pathfinding index.
				if(pathfindingIndex < master.mainPath.Count - 1) pathfindingIndex++;
				else {
					//Do something important involving
					//despawning etc.
				}
			}
		} else {
			if((path[0] - transform.position).sqrMagnitude < 3) {
				//Move to the next pathfinding index.
				path.RemoveAt(0);
			}
		}

        //Move towards the next node.
		if(rb.velocity.magnitude > 0.1f) {
			currentlyHit = false;
		} else if(rb.velocity.magnitude > 3f) {
			currentlyHit = true;
			useDefaultPath = false;
			path.Clear();
		}

        if (currentlyHit == false)
        {
			if(useDefaultPath) {
				//We can use the default path -
				//keep moving along it.
				rb.AddForce((master.mainPath[pathfindingIndex] - transform.position).normalized * force);
			} else {
				//We can't use the default path.
				//Get a new path leading towards
				//the correct node.
				if(path.Count == 0) {
					path = master.AStar(transform.position, master.mainPath[pathfindingIndex]);
				}
				else rb.AddForce((path[0] - transform.position).normalized * force);
			}
        }

	}

}
