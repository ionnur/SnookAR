using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMaster : MonoBehaviour {

	/* PathfindingMaster CLASS
	 * 
	 * You should only have one of these
	 * in the scene at once.
	 * 
	 * Attach all movement Transforms
	 * to this object. The BallPathfinding
	 * instances will use it to determine
	 * where they're going next.
	 * 
	 * -Kieran
	 * */

	[SerializeField]
	private Transform[] pathTransforms;

	private Vector2[] _path;

	public Vector2[] path {
		get { return _path; }
		private set { _path = value; }
	}

	// Use this for initialization
	void Start () {
		//Convert pathTransforms into cached vec2
		//for access optimisation.
		_path = new Vector2[pathTransforms.Length];
		for(int i = 0; i < pathTransforms.Length; i++) {
			_path[i] = new Vector2(pathTransforms[i].position.x, pathTransforms[i].position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
