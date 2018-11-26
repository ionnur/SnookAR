using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObstacle : MonoBehaviour {

	/* PathfindingObstacle CLASS
	 * 
	 * Carves the pathfinding array
	 * when the player places an obstacle,
	 * thereby causing the snooker balls
	 * to try and avoid the obstacle.
	 * 
	 * -Kieran
	 * */

	[SerializeField]
	protected Vector2 size;

	void Start () {
		PathfindingMaster master = FindObjectOfType<PathfindingMaster>();
		master.Carve(transform.position, size);
        Debug.Log("CARVED");
	}
}
