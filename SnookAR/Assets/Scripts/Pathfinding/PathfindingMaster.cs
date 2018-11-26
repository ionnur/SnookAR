using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;

public class PathfindingMaster : MonoBehaviour {

	/* PathfindingMaster CLASS
	 * 
	 * You should only have one of these
	 * in the scene at once.
	 * 
	 * -Kieran
	 * */

	[SerializeField]
	private Transform startPoint;

	[SerializeField]
	private Transform endPoint;


	private const float NODE_SCALE = 0.5f;

	private const int TABLE_WIDTH = 20;
	private const int TABLE_DEPTH = 60;

	private const int WIDTH_OFFSET = TABLE_WIDTH / 2;
	private const int DEPTH_OFFSET = TABLE_DEPTH / 2;

	//false by default. True if there's an obstacle
	//blocking the balls.
	public bool[,] pathfindingBlocked = new bool[TABLE_WIDTH, TABLE_DEPTH];

	//Arrays used by A*.
	private Vector2Int[,] previousNodes = new Vector2Int[TABLE_WIDTH, TABLE_DEPTH];
	private bool[,] visitedNodes = new bool[TABLE_WIDTH, TABLE_DEPTH];
	private float[,] startDists = new float[TABLE_WIDTH, TABLE_DEPTH];
	private float[,] endDists = new float[TABLE_WIDTH, TABLE_DEPTH];

	//The positions of each path point.
	public List<Vector3> mainPath = new List<Vector3>();

	/// <summary>
	/// Carve the pathfinding grid where an obstacle was placed.
	/// </summary>
	/// <param name="worldCentre">World centre of the obstacle.</param>
	/// <param name="dimensions">Radial dimensions of the object (m).</param>
	public void Carve(Vector3 worldCentre, Vector2 dimensions) {
		//Convert worldCentre into an index
		//of the pathfinding array.
		Vector2Int localCentre = GlobalToLocal(worldCentre);

		Vector2Int gridDimensions = new Vector2Int((int)(dimensions.x / NODE_SCALE), (int)(dimensions.y / NODE_SCALE));

		//Carve the pathfinding array at the
		//locations below the obstacle.
		for(int x = localCentre.x - gridDimensions.x; x <= localCentre.x + gridDimensions.x; x++) {
			for(int y = localCentre.y - gridDimensions.y; y <= localCentre.y + gridDimensions.y; y++) {
				//Ignore areas outside of the array.
				if(x < 0 || x >= TABLE_WIDTH || y < 0 || y >= TABLE_DEPTH) continue;

				pathfindingBlocked[x, y] = true;
			}
		}
	}

	private Vector2Int GlobalToLocal(Vector3 vecIn) {
		return new Vector2Int((int)(vecIn.x / NODE_SCALE) + WIDTH_OFFSET, (int)(vecIn.z / NODE_SCALE) + DEPTH_OFFSET);
	}

	private Vector3 LocalToGlobal(Vector2Int vecIn) {
		return new Vector3((vecIn.x - WIDTH_OFFSET) * NODE_SCALE, 0, (vecIn.y - DEPTH_OFFSET) * NODE_SCALE);
	}

	//Runs A* between two grid points,
	//returning each corner.
	private List<Vector2Int> InternalAStar(Vector2Int start, Vector2Int end) {
		List<Vector2Int> pathNodes = new List<Vector2Int>();
		List<Vector2Int> openNodes = new List<Vector2Int>();

		int nextIndex;
		float shortestDist;
		Vector2Int currentNode;
		bool impassable;

		//Initialise the A* arrays.
		for(int i = 0; i < TABLE_WIDTH; i++) {
			for(int j = 0; j < TABLE_DEPTH; j++) {
				previousNodes[i, j] = Vector2Int.one * -1;
				visitedNodes[i, j] = false;
				startDists[i, j] = float.PositiveInfinity;
				endDists[i, j] = float.PositiveInfinity;
			}
		}

		//Begin at the start.
		openNodes.Add(start);
        Debug.Log(start);
		startDists[start.x, start.y] = 0;
		endDists[start.x, start.y] = (start - end).sqrMagnitude;

		while(openNodes.Count > 0) {
			//Main A* loop.
			//Find the node within openNodes with
			//the lowest heuristic distance to the end.
			shortestDist = float.PositiveInfinity;
			nextIndex = 0;
			for(int i = 0; i < openNodes.Count; i++) {
				if(endDists[openNodes[i].x, openNodes[i].y] < shortestDist) {
					shortestDist = endDists[openNodes[i].x, openNodes[i].y];
					nextIndex = i;
				}
			}
			currentNode = openNodes[nextIndex];

			//Check if this node is the target.
			if(currentNode == end) {
				//Found the target. Return the path
				//leading to the target.
				List<Vector2Int> returnPath = new List<Vector2Int>();
				currentNode = end;
				while(previousNodes[currentNode.x, currentNode.y] != Vector2Int.one * -1) {
					returnPath.Insert(0, currentNode);
					currentNode = previousNodes[currentNode.x, currentNode.y];
				}
				return returnPath;
			};

			//Count this node as visited.
			openNodes.Remove(currentNode);
			visitedNodes[currentNode.x, currentNode.y] = true;

			//Visit the node by checking all of its
			//neighbours in turn.
			//Neighbours are 2 nodes away (A*16)
			//to yield smoother vectors.
			for(int x = -2; x <= 2; x++) {
				for(int y = -2; y <= 2; y++) {
					//Ignore non-edge nodes.
					if(Mathf.Abs(x) < 2 && Mathf.Abs(y) < 2) continue;
					//Ignore nodes out of index.
					if(currentNode.x + x < 0 || currentNode.x + x >= TABLE_WIDTH || 
						currentNode.y + y < 0 || currentNode.y + y >= TABLE_DEPTH) continue;
					//Check if the node was already visited.
					if(visitedNodes[currentNode.x + x, currentNode.y + y] == true) continue;

					//Check if the node, or any of the 8 spaces
					//adjacent to it, is impassable.
					impassable = false;
					for(int x2 = -1; x2 <= 1; x2++) {
						for(int y2 = -1; y2 <= 1; y2++) {
							//Ignore nodes out of index.
							if(currentNode.x + x + x2 < 0 || currentNode.x + x + x2 >= TABLE_WIDTH || 
								currentNode.y + y + y2 < 0 || currentNode.y + y + y2 >= TABLE_DEPTH) continue;

							if(pathfindingBlocked[currentNode.x + x + x2, currentNode.y + y + y2]) {
								//Path blocked. Consider this node visited.
								visitedNodes[currentNode.x + x, currentNode.y + y] = true;
								impassable = true;
							}
						}
					}
                    if (impassable) break;

					//If the node can be passed through,
					//and hasn't already been put in the open nodes,
					//add it to the open nodes.
					if(!openNodes.Contains(new Vector2Int(currentNode.x + x, currentNode.y + y))) {
						openNodes.Add(new Vector2Int(currentNode.x + x, currentNode.y + y));
					}

					//Update the heuristics for this node.
					float dist = startDists[currentNode.x, currentNode.y] + (x * x) + (y * y);
					if(dist < startDists[currentNode.x + x, currentNode.y + y]) {
						startDists[currentNode.x + x, currentNode.y + y] = dist;
						endDists[currentNode.x + x, currentNode.y + y] = dist + (currentNode + new Vector2Int(x, y) - end).sqrMagnitude;
						previousNodes[currentNode.x + x, currentNode.y + y] = currentNode;
					}
				}
			}
		}
		return new List<Vector2Int>();
	}

	//Wrapper for InternalAStar that uses world co-ordinates.
	public List<Vector3> AStar(Vector3 startPos, Vector3 endPos) {
		List<Vector2Int> tmp = InternalAStar(GlobalToLocal(startPos), GlobalToLocal(endPos));

		List<Vector3> returnList = new List<Vector3>();
		while(tmp.Count > 0) {
			returnList.Add(LocalToGlobal(tmp[0]));
			tmp.RemoveAt(0);
		}
		return returnList;
	}

    public bool TryAStar()
    {
        List<Vector3> tmp = AStar(startPoint.position, endPoint.position);
        //Debug.Log(tmp[tmp.Count - 1]);
        mainPath = tmp;
        if ((tmp[tmp.Count - 1] - endPoint.position).sqrMagnitude < 3)
        {
            return true;
        }
        else return false;
    }

	// Use this for initialization
	void Start () {
        TryAStar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
