using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{

	public Tilemap Map;
	public int Width = 200;
	public int Height = 200;
	public float timeToCompute = 5f;
	public float speed = 1;
	float timer = 0;
	Stack<Vector3> Path = new Stack<Vector3>();
	int tileCounter = 0; // THIS IS A TEST variable and should be changed

	Vector3 end = new Vector3();
	private bool pathfound;
	Vector3 nextPos;
	Type[] tiles = new Type[] { typeof(GoalTile), typeof(CashPointTile) };

	// Start is called before the first frame update
	void Start()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);
		pathfound = ComputePath(mapPos, tiles[0]);
		nextPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);
		//Debug.Log(string.Format("Tile: {0}, pos: {1}", tile, mapPos));
		Debug.DrawLine(transform.position, end);
		//if (timer >= timeToCompute)
		//{
		//	var pathFound = ComputePath(mapPos);
		//	timer = 0;
		//}	
		if (pathfound )//&& transform.position != end)
		{
			if (Vector3.Distance(transform.position, nextPos) <= 0.1 && Path.Count>0)
			{
				nextPos = Path.Pop();
			}
			transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
			//transform.position += -nextPos * speed * Time.deltaTime;
			if (Path.Count == 0)
			{
				timer += Time.deltaTime;
				if (timer >= timeToCompute)
				{
					tileCounter++;
					timer = 0;
					Debug.Log("i: " + tileCounter);
					pathfound = ComputePath(mapPos, tileCounter % 2 == 0 ? tiles[0] : tiles[1]);

				}
			}
		}
    }


	public IEnumerable<Vector3Int> Neightbors(Vector3Int current)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				yield return new Vector3Int(current.x + i, current.y+j, 0);
			}
		}
	}

	public bool ComputePath(Vector3Int startPosition, Type target)
	{
		var frontier = new Queue<Vector3Int>();
		frontier.Enqueue(startPosition);
		var visisted = new Dictionary<Vector3Int, bool>();
		var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
		Vector3Int goal = new Vector3Int();
		var goalFound = false;

		Debug.Log("starting");
		while (frontier.Count > 0 && !goalFound)
		{
			var current = frontier.Dequeue();
			visisted[current] = true;

			foreach (var point in Neightbors(current))
			{
				//Debug.DrawLine(transform.position, Map.GetCellCenterWorld(point), Color.red, 5);

				var tile = Map.GetTile(point);
				
				if (tile != null && tile.GetType() == (target))
				{
					if(tile is GoalTile && !((GoalTile)tile).pickedUP)
					{
						//this is a horrible hack and we should not talk about it.
						((GoalTile)tile).pickedUP = true;
						Debug.Log("FOUND TILE:" + point);
						//Map.SetTile(point, null);
						goal = point;
						goalFound = true;
						cameFrom[point] = current;
						break;
					}
					else if(!(tile is GoalTile))
					{
						Debug.Log("FOUND TILE:" + point);
						//Map.SetTile(point, null);
						goal = point;
						goalFound = true;
						cameFrom[point] = current;
						break;
					}
				}

				if (visisted.TryGetValue(point, out var x) || tile == null || tile.GetType() == typeof(WallTile) || point.x > Width || point.y > Height)
				{
					// out of bounds, we've been here, or we hit a wall
					continue;
				}

				frontier.Enqueue(point);
				visisted[point] = true;

				cameFrom[point] = current;
			}
			//backtrack
		}

		var currentPos = goal;
		var y = Map.WorldToCell(transform.position);
		Path.Push(Map.GetCellCenterWorld(currentPos));
		while (currentPos != y)
		{
			if(!cameFrom.TryGetValue(currentPos, out var x))
			{
				Debug.Log("FALSE");
				Path = new Stack<Vector3>(); // empty it. dunno if needed
				return false;
			}
			Path.Push(Map.GetCellCenterWorld(x));
			Debug.DrawLine(Map.GetCellCenterWorld(x), Map.GetCellCenterWorld(currentPos), Color.red, 5);

			currentPos = x;
		}
		end = Map.GetCellCenterWorld(goal);
		return true;	
	}

}
